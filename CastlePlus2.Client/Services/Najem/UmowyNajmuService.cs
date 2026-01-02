using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;

namespace CastlePlus2.Client.Services.Najem
{
    public class UmowyNajmuService : IUmowyNajmuService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/najem/UmowyNajmu";

        public UmowyNajmuService(HttpClient http)
        {
            _http = http;
        }

        public async Task<UmowaNajmuContextDto> GetContextAsync(CancellationToken ct = default)
        {
            return await _http.GetFromJsonAsync<UmowaNajmuContextDto>($"{BaseUrl}/context", ct)
                   ?? new UmowaNajmuContextDto();
        }

        public async Task<List<UmowaNajmuDto>> GetAllAsync(CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, BaseUrl);
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<List<UmowaNajmuDto>>(cancellationToken: ct) ?? new();
        }

        public async Task<UmowaNajmuDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/{id}");
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<UmowaNajmuDto>(cancellationToken: ct);
        }

        public async Task<UmowaNajmuDto> CreateAsync(CreateUmowaNajmuRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();

            return (await resp.Content.ReadFromJsonAsync<UmowaNajmuDto>(cancellationToken: ct))!;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateUmowaNajmuRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", request, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return false;

            resp.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{id}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return false;

            resp.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<ZawrzUmoweNajmuResult> ZawrzAsync(ZawrzUmoweNajmuRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync($"{BaseUrl}/zawrz", request, ct);

            // Sukces
            if (resp.IsSuccessStatusCode)
            {
                return (await resp.Content.ReadFromJsonAsync<ZawrzUmoweNajmuResult>(cancellationToken: ct))!;
            }

            // Każdy błąd: 400/409/500/itd — czytamy treść i zwracamy jako Message (UI pokaże w Snackbar)
            var msg =
                await TryReadValidationMessageAsync(resp, ct)
                ?? await TryReadMessageAsync(resp, ct)
                ?? await TryReadRawBodyAsync(resp, ct)
                ?? $"{(int)resp.StatusCode} {resp.ReasonPhrase}";

            return new ZawrzUmoweNajmuResult
            {
                Message = msg
            };
        }

        private static async Task<string?> TryReadValidationMessageAsync(HttpResponseMessage response, CancellationToken ct)
        {
            try
            {
                var json = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: ct);
                if (json.ValueKind != JsonValueKind.Object)
                    return null;

                // ValidationProblemDetails: { title, status, errors: { Field: [ "msg1", "msg2" ] } }
                if (json.TryGetProperty("errors", out var errors) && errors.ValueKind == JsonValueKind.Object)
                {
                    var sb = new StringBuilder();

                    foreach (var field in errors.EnumerateObject())
                    {
                        if (field.Value.ValueKind != JsonValueKind.Array)
                            continue;

                        foreach (var msg in field.Value.EnumerateArray())
                        {
                            var text = msg.GetString();
                            if (string.IsNullOrWhiteSpace(text))
                                continue;

                            if (sb.Length > 0) sb.Append(" | ");
                            sb.Append(field.Name).Append(": ").Append(text);
                        }
                    }

                    if (sb.Length > 0)
                        return sb.ToString();
                }

                // fallback: ProblemDetails / custom
                if (json.TryGetProperty("message", out var message)) return message.GetString();
                if (json.TryGetProperty("detail", out var detail)) return detail.GetString();
                if (json.TryGetProperty("title", out var title)) return title.GetString();
            }
            catch (JsonException) { }
            catch (NotSupportedException) { }

            return null;
        }

        private static async Task<string?> TryReadMessageAsync(HttpResponseMessage response, CancellationToken ct)
        {
            try
            {
                var json = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: ct);
                if (json.ValueKind == JsonValueKind.Object)
                {
                    if (json.TryGetProperty("message", out var message)) return message.GetString();
                    if (json.TryGetProperty("detail", out var detail)) return detail.GetString();
                    if (json.TryGetProperty("title", out var title)) return title.GetString();
                }
            }
            catch (JsonException) { }
            catch (NotSupportedException) { }

            return null;
        }

        private static async Task<string?> TryReadRawBodyAsync(HttpResponseMessage response, CancellationToken ct)
        {
            try
            {
                var body = await response.Content.ReadAsStringAsync(ct);
                if (string.IsNullOrWhiteSpace(body))
                    return null;

                // żeby snackbar nie dostał gigantycznego tekstu
                return body.Length <= 1500 ? body : body.Substring(0, 1500);
            }
            catch
            {
                return null;
            }
        }
    }
}
