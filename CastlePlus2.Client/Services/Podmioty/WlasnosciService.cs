using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;

namespace CastlePlus2.Client.Services.Podmioty
{
    public class WlasnosciService : IWlasnosciService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/podmioty/wlasnosci";

        public WlasnosciService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<WlasnoscDto>> GetAllAsync(CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, BaseUrl);
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<List<WlasnoscDto>>(cancellationToken: ct) ?? new();
        }

        public async Task<WlasnoscDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/{id}");
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<WlasnoscDto>(cancellationToken: ct);
        }

        public async Task<List<WlasnoscDto>> GetByEncjaAsync(Guid idEncji, CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/by-encja/{idEncji}");
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<List<WlasnoscDto>>(cancellationToken: ct) ?? new();
        }

        public async Task<WlasnoscDto> CreateAsync(CreateWlasnoscRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();

            var dto = await resp.Content.ReadFromJsonAsync<WlasnoscDto>(cancellationToken: ct);
            return dto ?? throw new InvalidOperationException("API zwróciło pustą odpowiedź (WlasnoscDto = null).");
        }

        public async Task<WlasnoscDto?> UpdateAsync(long id, UpdateWlasnoscRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", request, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<WlasnoscDto>(cancellationToken: ct);
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