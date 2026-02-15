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
        private const string FakturyBaseUrl = "api/finanse/procesy/faktury";
        private const string PlatnosciBaseUrl = "api/finanse/procesy/platnosci";

        public ProcesyFinanseService(HttpClient http, ILogger<ProcesyFinanseService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<WystawFaktureContextDto> GetWystawFaktureContextAsync(CancellationToken ct = default)
            => await _http.GetFromJsonAsync<WystawFaktureContextDto>($"{FakturyBaseUrl}/context", ct) ?? new();

        public async Task<WystawFaktureResultDto> WystawFaktureAsync(WystawFaktureRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync($"{FakturyBaseUrl}/wystaw", request, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Wystawienie faktury nie powiodło się: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }

            return (await resp.Content.ReadFromJsonAsync<WystawFaktureResultDto>(cancellationToken: ct))!;
        }

        public async Task<GenerateNajemFakturyResultDto> GenerateNajemFakturyAsync(GenerateNajemFakturyRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync($"{FakturyBaseUrl}/najem/generate", request, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Generowanie faktur najmu nie powiodło się: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }

            return (await resp.Content.ReadFromJsonAsync<GenerateNajemFakturyResultDto>(cancellationToken: ct))!;
        }

        public async Task<PlatnoscContextDto> GetPlatnoscContextAsync(CancellationToken ct = default)
            => await _http.GetFromJsonAsync<PlatnoscContextDto>($"{PlatnosciBaseUrl}/context", ct) ?? new();

        public async Task<ZarejestrujPlatnoscResultDto> ZarejestrujPlatnoscAsync(ZarejestrujPlatnoscRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync($"{PlatnosciBaseUrl}/zarejestruj", request, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Rejestracja płatności nie powiodła się: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }

            return (await resp.Content.ReadFromJsonAsync<ZarejestrujPlatnoscResultDto>(cancellationToken: ct))!;
        }
    }
}