using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Slowniki;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Slowniki
{
    public class IndeksacjeService : IIndeksacjeService
    {
        private readonly HttpClient _http;
        private readonly ILogger<IndeksacjeService> _logger;

        private const string BaseUrl = "api/slowniki/Indeksacje";

        public IndeksacjeService(HttpClient http, ILogger<IndeksacjeService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<IndeksacjaDto>> GetAllAsync(CancellationToken ct = default)
        {
            return (await _http.GetFromJsonAsync<List<IndeksacjaDto>>(BaseUrl, ct)) ?? new();
        }

        public async Task<IndeksacjaDto?> GetByKodAsync(string kod, CancellationToken ct = default)
        {
            var resp = await _http.GetAsync($"{BaseUrl}/{Uri.EscapeDataString(kod)}", ct);
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<IndeksacjaDto>(cancellationToken: ct);
        }

        public async Task<IndeksacjaDto> CreateAsync(CreateIndeksacjaRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Create Indeksacja failed: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }

            return (await resp.Content.ReadFromJsonAsync<IndeksacjaDto>(cancellationToken: ct))!;
        }

        public async Task UpdateAsync(string kod, UpdateIndeksacjaRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{Uri.EscapeDataString(kod)}", request, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Update Indeksacja failed: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteAsync(string kod, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{Uri.EscapeDataString(kod)}", ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Delete Indeksacja failed: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }
        }
    }
}
