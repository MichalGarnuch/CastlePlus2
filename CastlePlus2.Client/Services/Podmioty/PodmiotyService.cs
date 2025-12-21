using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;

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

        public async Task<long> CreateAsync(CreatePodmiotRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();
            var dto = await resp.Content.ReadFromJsonAsync<PodmiotDto>(cancellationToken: ct);
            return dto!.IdPodmiotu;
        }

        public async Task<bool> UpdateAsync(long id, UpdatePodmiotRequest request, CancellationToken ct = default)
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
