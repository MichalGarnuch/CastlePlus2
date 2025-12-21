using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;

namespace CastlePlus2.Client.Services.Rdzen
{
    public class PrzypisaniaAdresowService : IPrzypisaniaAdresowService
    {
        private readonly HttpClient _http;

        // zgodnie z kontrolerem: [Route("api/[controller]")] => api/PrzypisaniaAdresow
        private const string BaseUrl = "api/PrzypisaniaAdresow";

        public PrzypisaniaAdresowService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PrzypisanieAdresuDto>> GetAllAsync(CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, BaseUrl);
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<List<PrzypisanieAdresuDto>>(cancellationToken: ct) ?? new();
        }

        public async Task<PrzypisanieAdresuDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/{id}");
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<PrzypisanieAdresuDto>(cancellationToken: ct);
        }

        public async Task<long> CreateAsync(CreatePrzypisanieAdresuRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();

            var dto = await resp.Content.ReadFromJsonAsync<PrzypisanieAdresuDto>(cancellationToken: ct);
            return dto!.IdPrzypisaniaAdresu;
        }

        public async Task<bool> UpdateAsync(long id, UpdatePrzypisanieAdresuRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", request, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return false;

            resp.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{id}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return false;

            resp.EnsureSuccessStatusCode();
            return true;
        }
    }
}
