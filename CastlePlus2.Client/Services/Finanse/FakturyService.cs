using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;

namespace CastlePlus2.Client.Services.Finanse
{
    public class FakturyService : IFakturyService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/finanse/faktury";

        public FakturyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<FakturaDto>> GetAllAsync(CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, BaseUrl);
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<List<FakturaDto>>(cancellationToken: ct) ?? new();
        }

        public async Task<FakturaDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var resp = await _http.GetAsync($"{BaseUrl}/{id}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<FakturaDto>(cancellationToken: ct);
        }

        public async Task<FakturaDto> CreateAsync(CreateFakturaRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();

            var dto = await resp.Content.ReadFromJsonAsync<FakturaDto>(cancellationToken: ct);
            return dto ?? throw new InvalidOperationException("API zwróciło pustą odpowiedź (FakturaDto = null).");
        }

        public async Task<FakturaDto?> UpdateAsync(long id, UpdateFakturaRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", request, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<FakturaDto>(cancellationToken: ct);
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
