using System.Net;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;

namespace CastlePlus2.Client.Services.Rdzen
{
    public class LokaleService : ILokaleService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/Lokale";

        public LokaleService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<LokalDto>> GetAllAsync(CancellationToken ct = default)
            => await _http.GetFromJsonAsync<List<LokalDto>>(BaseUrl, ct) ?? new();

        public async Task<LokalDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var resp = await _http.GetAsync($"{BaseUrl}/{id}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<LokalDto>(cancellationToken: ct);
        }

        public async Task<Guid> CreateAsync(CreateLokalRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();
            var dto = await resp.Content.ReadFromJsonAsync<LokalDto>(cancellationToken: ct);
            return dto!.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateLokalRequest request, CancellationToken ct = default)
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
