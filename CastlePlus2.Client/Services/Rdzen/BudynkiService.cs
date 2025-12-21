using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;

namespace CastlePlus2.Client.Services.Rdzen
{
    public class BudynkiService : IBudynkiService
    {
        private readonly HttpClient _http;

        // Jeżeli kontroler ma [Route("api/[controller]")] i nazywa się BudynkiController => "api/Budynki"
        private const string BaseUrl = "api/Budynki";

        public BudynkiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<BudynekDto>> GetAllAsync(CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, BaseUrl);
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<List<BudynekDto>>(cancellationToken: ct) ?? new();
        }

        public async Task<BudynekDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/{id}");
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<BudynekDto>(cancellationToken: ct);
        }

        public async Task<Guid> CreateAsync(CreateBudynekRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();

            var dto = await resp.Content.ReadFromJsonAsync<BudynekDto>(cancellationToken: ct);
            return dto!.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateBudynekRequest request, CancellationToken ct = default)
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
