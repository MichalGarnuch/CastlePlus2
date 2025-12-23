using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;

namespace CastlePlus2.Client.Services.Podmioty
{
    public class KontaktyService : IKontaktyService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/podmioty/kontakty";

        public KontaktyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<KontaktDto>> GetByPodmiotIdAsync(long idPodmiotu, CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/by-podmiot/{idPodmiotu}");
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<List<KontaktDto>>(cancellationToken: ct) ?? new();
        }

        public async Task<KontaktDto?> GetByIdAsync(long idKontaktu, CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/{idKontaktu}");
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<KontaktDto>(cancellationToken: ct);
        }

        public async Task<long> CreateAsync(CreateKontaktRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();
            var dto = await resp.Content.ReadFromJsonAsync<KontaktDto>(cancellationToken: ct);
            return dto!.IdKontaktu;
        }

        public async Task<bool> UpdateAsync(long idKontaktu, UpdateKontaktRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{idKontaktu}", request, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return false;
            resp.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> DeleteAsync(long idKontaktu, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{idKontaktu}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return false;
            resp.EnsureSuccessStatusCode();
            return true;
        }
    }
}