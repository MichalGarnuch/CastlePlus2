using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Finanse
{
    public class ProcesyFinanseService : IProcesyFinanseService
    {
        private readonly HttpClient _http;
        private readonly ILogger<ProcesyFinanseService> _logger;
        private const string BaseUrl = "api/finanse/procesy/faktury";

        public ProcesyFinanseService(HttpClient http, ILogger<ProcesyFinanseService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<WystawFaktureContextDto> GetWystawFaktureContextAsync(CancellationToken ct = default)
            => await _http.GetFromJsonAsync<WystawFaktureContextDto>($"{BaseUrl}/context", ct) ?? new();

        public async Task<WystawFaktureResultDto> WystawFaktureAsync(WystawFaktureRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync($"{BaseUrl}/wystaw", request, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Wystawienie faktury nie powiodło się: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }

            return (await resp.Content.ReadFromJsonAsync<WystawFaktureResultDto>(cancellationToken: ct))!;
        }
    }
}