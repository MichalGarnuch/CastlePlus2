using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Linq;
using CastlePlus2.Client.Services.Common;
using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Client.Services.Podmioty
{
    public class PodmiotyService : IPodmiotyService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/podmioty/podmioty";

        public PodmiotyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PodmiotDto>> GetAllAsync(CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, BaseUrl);
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<List<PodmiotDto>>(cancellationToken: ct) ?? new();
        }

        public async Task<PodmiotPagedResultDto> GetPagedAsync(
            int page,
            int pageSize,
            string? searchTerm,
            string? sortBy,
            bool sortDesc,
            CancellationToken ct = default)
        {
            var url = $"{BaseUrl}/paged?page={page}&pageSize={pageSize}";

            if (!string.IsNullOrWhiteSpace(searchTerm))
                url += $"&searchTerm={Uri.EscapeDataString(searchTerm)}";

            if (!string.IsNullOrWhiteSpace(sortBy))
                url += $"&sortBy={Uri.EscapeDataString(sortBy)}&sortDesc={sortDesc}";

            using var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<PodmiotPagedResultDto>(cancellationToken: ct)
                   ?? new PodmiotPagedResultDto();
        }

        public async Task<List<PodmiotDto>> SearchAsync(string searchTerm, int take, CancellationToken ct = default)
        {
            var result = await GetPagedAsync(1, take, searchTerm, "Nazwa", false, ct);
            return result.Items;
        }

        public async Task<PagedResultDto<PodmiotLookupDto>> SearchLookupPagedAsync(
            string? q,
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            var currentPage = page <= 0 ? 1 : page;
            var currentPageSize = pageSize <= 0 ? 20 : Math.Min(pageSize, 200);
            var url = $"{BaseUrl}/lookup?page={currentPage}&pageSize={currentPageSize}";

            if (!string.IsNullOrWhiteSpace(q))
                url += $"&q={Uri.EscapeDataString(q)}";

            return await _http.GetFromJsonAsync<PagedResultDto<PodmiotLookupDto>>(url, ct)
                   ?? new PagedResultDto<PodmiotLookupDto>();
        }

        public async Task<PodmiotDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/{id}");
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<PodmiotDto>(cancellationToken: ct);
        }

        public async Task<PodmiotDto> CreateAsync(CreatePodmiotRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            if (resp.IsSuccessStatusCode)
            {
                var dto = await resp.Content.ReadFromJsonAsync<PodmiotDto>(cancellationToken: ct);
                return dto!;
            }

            if (resp.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = await TryReadValidationErrorsAsync(resp, ct);
                if (errors != null)
                    throw new ApiValidationException("Błąd walidacji danych podmiotu.", errors);
            }

            await ThrowForErrorAsync(resp, "Nie udało się dodać podmiotu.", ct);
            return null!;
        }

        public async Task<bool> UpdateAsync(long id, UpdatePodmiotRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", request, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return false;
            if (resp.IsSuccessStatusCode) return true;

            if (resp.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = await TryReadValidationErrorsAsync(resp, ct);
                if (errors != null)
                    throw new ApiValidationException("Błąd walidacji danych podmiotu.", errors);
            }

            await ThrowForErrorAsync(resp, "Nie udało się zapisać zmian podmiotu.", ct);
            return true;
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{id}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return false;
            resp.EnsureSuccessStatusCode();
            return true;
        }

        private static async Task<IReadOnlyDictionary<string, string[]>?> TryReadValidationErrorsAsync(
            HttpResponseMessage response,
            CancellationToken ct)
        {
            try
            {
                var json = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: ct);
                if (json.ValueKind != JsonValueKind.Object)
                    return null;

                if (json.TryGetProperty("errors", out var errors) && errors.ValueKind == JsonValueKind.Object)
                {
                    var dict = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

                    foreach (var field in errors.EnumerateObject())
                    {
                        if (field.Value.ValueKind != JsonValueKind.Array)
                            continue;

                        var messages = field.Value.EnumerateArray()
                            .Select(x => x.GetString())
                            .Where(x => !string.IsNullOrWhiteSpace(x))
                            .Select(x => x!)
                            .ToArray();

                        if (messages.Length > 0)
                            dict[field.Name] = messages;
                    }

                    return dict.Count > 0 ? dict : null;
                }
            }
            catch (JsonException) { }
            catch (NotSupportedException) { }

            return null;
        }

        private static async Task ThrowForErrorAsync(HttpResponseMessage response, string fallbackMessage, CancellationToken ct)
        {
            string? message = null;

            try
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken: ct);
                if (!string.IsNullOrWhiteSpace(problem?.Detail)) message = problem.Detail;
                else if (!string.IsNullOrWhiteSpace(problem?.Title)) message = problem.Title;
            }
            catch { }

            message ??= fallbackMessage;
            throw new InvalidOperationException(message);
        }
    }
}
