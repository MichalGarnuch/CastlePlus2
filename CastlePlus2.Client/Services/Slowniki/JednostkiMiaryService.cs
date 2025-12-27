using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Slowniki;
using CastlePlus2.Contracts.Requests.Slownik;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Slowniki
{
    public class JednostkiMiaryService : IJednostkiMiaryService
    {
        private readonly HttpClient _http;
        private readonly ILogger<JednostkiMiaryService> _logger;

        private const string BaseUrl = "api/slowniki/JednostkiMiary";

        public JednostkiMiaryService(HttpClient http, ILogger<JednostkiMiaryService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<JednostkaMiaryDto>> GetAllAsync(CancellationToken ct = default)
            => await _http.GetFromJsonAsync<List<JednostkaMiaryDto>>(BaseUrl, ct) ?? new();

        public async Task<JednostkaMiaryDto?> GetByKodAsync(string kodJednostki, CancellationToken ct = default)
            => await _http.GetFromJsonAsync<JednostkaMiaryDto>($"{BaseUrl}/{Uri.EscapeDataString(kodJednostki)}", ct);

        public async Task<JednostkaMiaryDto> CreateAsync(CreateJednostkaMiaryRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Create JednostkaMiary failed: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }

            return (await resp.Content.ReadFromJsonAsync<JednostkaMiaryDto>(cancellationToken: ct))!;
        }

        public async Task UpdateAsync(string kodJednostki, UpdateJednostkaMiaryRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{Uri.EscapeDataString(kodJednostki)}", request, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Update JednostkaMiary failed: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteAsync(string kodJednostki, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{Uri.EscapeDataString(kodJednostki)}", ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Delete JednostkaMiary failed: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }
        }
    }
}
