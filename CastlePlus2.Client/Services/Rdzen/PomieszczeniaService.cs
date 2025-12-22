using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;

namespace CastlePlus2.Client.Services.Rdzen
{
    public class PomieszczeniaService : IPomieszczeniaService
    {
        private readonly HttpClient _http;

        // UWAGA: to MUSI zgadzać się z Twoim routingiem kontrolera.
        // Jeśli kontroler ma [Route("api/[controller]")] i nazywa się PomieszczeniaController => api/Pomieszczenia
        private const string BaseUrl = "api/Pomieszczenia";

        public PomieszczeniaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PomieszczenieDto>> GetAllAsync(CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, BaseUrl);
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<List<PomieszczenieDto>>(cancellationToken: ct) ?? new();
        }

        public async Task<PomieszczenieDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/{id}");
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<PomieszczenieDto>(cancellationToken: ct);
        }

        public async Task<Guid> CreateAsync(CreatePomieszczenieRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();

            // Czytamy "surowy" JSON, bo API może zwracać:
            // 1) sam Guid (np. "b7f1..."), albo
            // 2) obiekt DTO { "id": "...", ... } / { "Id": "...", ... }
            var json = await resp.Content.ReadAsStringAsync(ct);

            if (string.IsNullOrWhiteSpace(json))
                throw new InvalidOperationException("API zwróciło pustą odpowiedź dla POST (oczekiwano Id).");

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            // przypadek 1: "guid-string"
            if (root.ValueKind == JsonValueKind.String)
            {
                var s = root.GetString();
                if (Guid.TryParse(s, out var gid))
                    return gid;
            }

            // przypadek 2: { "id": "guid" } lub { "Id": "guid" }
            if (root.ValueKind == JsonValueKind.Object)
            {
                if (root.TryGetProperty("id", out var idLower) && idLower.ValueKind == JsonValueKind.String)
                {
                    var s = idLower.GetString();
                    if (Guid.TryParse(s, out var gid))
                        return gid;
                }

                if (root.TryGetProperty("Id", out var idUpper) && idUpper.ValueKind == JsonValueKind.String)
                {
                    var s = idUpper.GetString();
                    if (Guid.TryParse(s, out var gid))
                        return gid;
                }
            }

            throw new InvalidOperationException($"Nie rozpoznano formatu odpowiedzi POST. Oczekiwano Guid albo obiektu z polem Id/id. Odpowiedź: {json}");
        }


        public async Task<bool> UpdateAsync(Guid id, UpdatePomieszczenieRequest request, CancellationToken ct = default)
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
    }
}
