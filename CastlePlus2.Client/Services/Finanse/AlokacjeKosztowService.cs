using System.Net;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;

namespace CastlePlus2.Client.Services.Finanse
{
    public class AlokacjeKosztowService : IAlokacjeKosztowService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/finanse/alokacjekosztow";

        public AlokacjeKosztowService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<AlokacjaKosztuDto>> GetAllAsync(CancellationToken ct = default)
        {
            return await _http.GetFromJsonAsync<List<AlokacjaKosztuDto>>(BaseUrl, ct)
                   ?? new List<AlokacjaKosztuDto>();
        }

        public async Task<AlokacjaKosztuDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var resp = await _http.GetAsync($"{BaseUrl}/{id}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<AlokacjaKosztuDto>(cancellationToken: ct);
        }

        public async Task<AlokacjaKosztuDto> CreateAsync(CreateAlokacjaKosztuRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();

            var dto = await resp.Content.ReadFromJsonAsync<AlokacjaKosztuDto>(cancellationToken: ct);
            return dto ?? throw new InvalidOperationException("API zwróciło pustą odpowiedź dla Create.");
        }

        public async Task<bool> UpdateAsync(long id, UpdateAlokacjaKosztuRequest request, CancellationToken ct = default)
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
