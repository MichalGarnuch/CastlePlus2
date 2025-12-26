using System.Net;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Contracts.Requests.Dokumenty;

namespace CastlePlus2.Client.Services.Dokumenty
{
    public class PowiazaniaDokumentuService : IPowiazaniaDokumentuService
    {
        private readonly HttpClient _http;

        // Controller: api/dokumenty/powiazanadokumentu
        private const string BaseUrl = "api/dokumenty/powiazaniadokumentu";

        public PowiazaniaDokumentuService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PowiazanieDokumentuDto>> GetAllAsync(CancellationToken ct = default)
        {
            return await _http.GetFromJsonAsync<List<PowiazanieDokumentuDto>>(BaseUrl, ct)
                   ?? new List<PowiazanieDokumentuDto>();
        }

        public async Task<PowiazanieDokumentuDto?> GetByIdAsync(long idPowiazania, CancellationToken ct = default)
        {
            var resp = await _http.GetAsync($"{BaseUrl}/{idPowiazania}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<PowiazanieDokumentuDto>(cancellationToken: ct);
        }

        public async Task<PowiazanieDokumentuDto> CreateAsync(CreatePowiazanieDokumentuRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();

            return (await resp.Content.ReadFromJsonAsync<PowiazanieDokumentuDto>(cancellationToken: ct))!;
        }

        public async Task<bool> UpdateAsync(long idPowiazania, UpdatePowiazanieDokumentuRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{idPowiazania}", request, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return false;

            resp.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> DeleteAsync(long idPowiazania, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{idPowiazania}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return false;

            resp.EnsureSuccessStatusCode();
            return true;
        }
    }
}
