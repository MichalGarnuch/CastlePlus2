using System;
using System.Net;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using System.Net.Http.Headers;


namespace CastlePlus2.Client.Services.Rdzen
{
    public class AdresyService : IAdresyService
    {
        private readonly HttpClient _http;

        // endpoint z kontrolera: api/rdzen/adresy
        private const string BaseUrl = "api/rdzen/adresy";

        public AdresyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<AdresDto>> GetAllAsync(CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, BaseUrl);
            req.Headers.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true
            };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<List<AdresDto>>(cancellationToken: ct)
                   ?? new List<AdresDto>();
        }


        public async Task<AdresDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/{id}");
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);

            if (resp.StatusCode == HttpStatusCode.NotFound) return null;
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<AdresDto>(cancellationToken: ct);
        }

        public async Task<AdresDto> CreateAsync(CreateAdresRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();
            var dto = await resp.Content.ReadFromJsonAsync<AdresDto>(cancellationToken: ct);
            return dto ?? throw new InvalidOperationException("Brak danych adresu w odpowiedzi.");
        }

        public async Task<bool> UpdateAsync(long id, UpdateAdresRequest request, CancellationToken ct = default)
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
