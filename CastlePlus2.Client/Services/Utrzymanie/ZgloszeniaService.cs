using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Contracts.Requests.Utrzymanie;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Utrzymanie
{
    public class ZgloszeniaService : IZgloszeniaService
    {
        private readonly HttpClient _http;
        private readonly ILogger<ZgloszeniaService> _logger;

        private const string BaseUrl = "api/utrzymanie/zgloszenia";

        public ZgloszeniaService(HttpClient http, ILogger<ZgloszeniaService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<ZglosUsterkeContextDto> GetContextAsync(CancellationToken ct = default)
            => await _http.GetFromJsonAsync<ZglosUsterkeContextDto>($"{BaseUrl}/context", ct) ?? new();

        public async Task<ZglosUsterkeResult> CreateAsync(ZglosUsterkeRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("ZglosUsterke failed: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }

            return (await resp.Content.ReadFromJsonAsync<ZglosUsterkeResult>(cancellationToken: ct))!;
        }
    }
}