using System.Net;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;

namespace CastlePlus2.Client.Services.Finanse
{
    public class RozliczeniaPlatnosciService : IRozliczeniaPlatnosciService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/finanse/rozliczeniaplatnosci";

        public RozliczeniaPlatnosciService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<RozliczeniePlatnosciDto>> GetAllAsync(CancellationToken ct = default)
        {
            return await _http.GetFromJsonAsync<List<RozliczeniePlatnosciDto>>(BaseUrl, ct)
                   ?? new List<RozliczeniePlatnosciDto>();
        }

        public async Task<RozliczeniePlatnosciDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var resp = await _http.GetAsync($"{BaseUrl}/{id}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<RozliczeniePlatnosciDto>(cancellationToken: ct);
        }

        public async Task<RozliczeniePlatnosciDto> CreateAsync(CreateRozliczeniePlatnosciRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();
            return (await resp.Content.ReadFromJsonAsync<RozliczeniePlatnosciDto>(cancellationToken: ct))!;
        }

        public async Task<RozliczeniePlatnosciDto?> UpdateAsync(long id, UpdateRozliczeniePlatnosciRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", request, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<RozliczeniePlatnosciDto>(cancellationToken: ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{id}", ct);
            resp.EnsureSuccessStatusCode();
        }
    }
}
