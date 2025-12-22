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

        public async Task<PomieszczenieDto> CreateAsync(CreatePomieszczenieRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();

            var dto = await resp.Content.ReadFromJsonAsync<PomieszczenieDto>(cancellationToken: ct);
            return dto!;
        }

        public async Task<PomieszczenieDto?> UpdateAsync(Guid id, UpdatePomieszczenieRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", request, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            if (resp.Content.Headers.ContentLength is null || resp.Content.Headers.ContentLength == 0)
                return await GetByIdAsync(id, ct);

            return await resp.Content.ReadFromJsonAsync<PomieszczenieDto>(cancellationToken: ct);
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
