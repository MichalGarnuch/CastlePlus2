using System.Net;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;

namespace CastlePlus2.Client.Services.Finanse
{
    public class KategorieKosztowService : IKategorieKosztowService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/finanse/kategoriekosztow";

        public KategorieKosztowService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<KategoriaKosztuDto>> GetAllAsync(CancellationToken ct = default)
        {
            return await _http.GetFromJsonAsync<List<KategoriaKosztuDto>>(BaseUrl, ct)
                   ?? new List<KategoriaKosztuDto>();
        }

        public async Task<KategoriaKosztuDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var resp = await _http.GetAsync($"{BaseUrl}/{id}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<KategoriaKosztuDto>(cancellationToken: ct);
        }

        public async Task<KategoriaKosztuDto> CreateAsync(CreateKategoriaKosztuRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();
            return (await resp.Content.ReadFromJsonAsync<KategoriaKosztuDto>(cancellationToken: ct))!;
        }

        public async Task<KategoriaKosztuDto?> UpdateAsync(long id, UpdateKategoriaKosztuRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", request, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<KategoriaKosztuDto>(cancellationToken: ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{id}", ct);
            resp.EnsureSuccessStatusCode();
        }
    }
}
