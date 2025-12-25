using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Contracts.Requests.Utrzymanie;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Utrzymanie
{
    public class ZleceniaPracyService : IZleceniaPracyService
    {
        private readonly HttpClient _http;
        private readonly ILogger<ZleceniaPracyService> _logger;

        private const string BaseUrl = "api/ZleceniaPracy";

        public ZleceniaPracyService(HttpClient http, ILogger<ZleceniaPracyService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<ZleceniePracyDto>> GetAllAsync(CancellationToken ct = default)
            => await _http.GetFromJsonAsync<List<ZleceniePracyDto>>(BaseUrl, ct) ?? new();

        public async Task<ZleceniePracyDto?> GetByIdAsync(long id, CancellationToken ct = default)
            => await _http.GetFromJsonAsync<ZleceniePracyDto>($"{BaseUrl}/{id}", ct);

        public async Task<ZleceniePracyDto> CreateAsync(CreateZleceniePracyRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Create ZleceniePracy failed: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }

            return (await resp.Content.ReadFromJsonAsync<ZleceniePracyDto>(cancellationToken: ct))!;
        }

        public async Task<ZleceniePracyDto?> UpdateAsync(long id, UpdateZleceniePracyRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", request, ct);
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Update ZleceniePracy failed: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }

            return await resp.Content.ReadFromJsonAsync<ZleceniePracyDto>(cancellationToken: ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{id}", ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Delete ZleceniePracy failed: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }
        }
    }
}