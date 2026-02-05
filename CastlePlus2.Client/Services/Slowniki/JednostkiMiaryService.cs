using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CastlePlus2.Client.Services.Common;
using CastlePlus2.Contracts.DTOs.Slowniki;
using CastlePlus2.Contracts.Requests.Slownik;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Slowniki
{
    public class JednostkiMiaryService : IJednostkiMiaryService
    {
        private readonly HttpClient _http;
        private readonly ILogger<JednostkiMiaryService> _logger;

        private const string BaseUrl = "api/slowniki/JednostkiMiary";

        public JednostkiMiaryService(HttpClient http, ILogger<JednostkiMiaryService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<JednostkaMiaryDto>> GetAllAsync(CancellationToken ct = default)
            => await _http.GetFromJsonAsync<List<JednostkaMiaryDto>>(BaseUrl, ct) ?? new();

        public async Task<JednostkaMiaryDto?> GetByKodAsync(string kodJednostki, CancellationToken ct = default)
            => await _http.GetFromJsonAsync<JednostkaMiaryDto>($"{BaseUrl}/{Uri.EscapeDataString(kodJednostki)}", ct);

        public async Task<JednostkaMiaryDto> CreateAsync(CreateJednostkaMiaryRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            if (resp.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = await TryReadValidationErrorsAsync(resp, ct);
                if (errors is not null)
                    throw new ApiValidationException("Błąd walidacji danych jednostki miary.", errors);
            }

            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Create JednostkaMiary failed: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }

            return (await resp.Content.ReadFromJsonAsync<JednostkaMiaryDto>(cancellationToken: ct))!;
        }

        public async Task UpdateAsync(string kodJednostki, UpdateJednostkaMiaryRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{Uri.EscapeDataString(kodJednostki)}", request, ct);
            if (resp.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = await TryReadValidationErrorsAsync(resp, ct);
                if (errors is not null)
                    throw new ApiValidationException("Błąd walidacji danych jednostki miary.", errors);
            }

            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Update JednostkaMiary failed: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteAsync(string kodJednostki, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{Uri.EscapeDataString(kodJednostki)}", ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Delete JednostkaMiary failed: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }
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
    }
}
