using System.Net;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Contracts.Requests.Dokumenty;

namespace CastlePlus2.Client.Services.Dokumenty
{
    public class DokumentyService : IDokumentyService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/dokumenty";

        public DokumentyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<DokumentDto>> GetAllAsync(CancellationToken ct = default)
        {
            return await _http.GetFromJsonAsync<List<DokumentDto>>(BaseUrl, ct)
                   ?? new List<DokumentDto>();
        }

        public async Task<DokumentDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var resp = await _http.GetAsync($"{BaseUrl}/{id}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<DokumentDto>(cancellationToken: ct);
        }

        public async Task<DokumentDto> CreateAsync(CreateDokumentRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();
            return (await resp.Content.ReadFromJsonAsync<DokumentDto>(cancellationToken: ct))!;
        }

        public async Task<bool> UpdateAsync(long id, UpdateDokumentRequest request, CancellationToken ct = default)
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
