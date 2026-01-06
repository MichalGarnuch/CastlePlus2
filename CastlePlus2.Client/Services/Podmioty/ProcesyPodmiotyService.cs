using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace CastlePlus2.Client.Services.Podmioty
{
    public class ProcesyPodmiotyService : IProcesyPodmiotyService
    {
        private const string BaseUrl = "api/podmioty/procesy/wlasnosc";
        private readonly HttpClient _http;
        private readonly ILogger<ProcesyPodmiotyService> _logger;

        public ProcesyPodmiotyService(HttpClient http, ILogger<ProcesyPodmiotyService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<WlasnoscContextDto> GetWlasnoscContextAsync(CancellationToken ct = default)
            => await _http.GetFromJsonAsync<WlasnoscContextDto>($"{BaseUrl}/context", ct) ?? new();

        public async Task<IReadOnlyList<WlasnoscDto>> UstawWlasnoscAsync(UstawWlasnoscRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync($"{BaseUrl}/ustaw", request, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Ustawienie własności nie powiodło się: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }

            return (await resp.Content.ReadFromJsonAsync<List<WlasnoscDto>>(cancellationToken: ct)) ?? new List<WlasnoscDto>();
        }
    }
}