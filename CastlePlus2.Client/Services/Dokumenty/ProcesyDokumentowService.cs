using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Contracts.Requests.Dokumenty;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Dokumenty
{
    public class ProcesyDokumentowService : IProcesyDokumentowService
    {
        private readonly HttpClient _http;
        private readonly ILogger<ProcesyDokumentowService> _logger;
        private const string BaseUrl = "api/dokumenty/procesy/rejestracja";

        public ProcesyDokumentowService(HttpClient http, ILogger<ProcesyDokumentowService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<RegisterDokumentContextDto> GetRegisterContextAsync(CancellationToken ct = default)
            => await _http.GetFromJsonAsync<RegisterDokumentContextDto>($"{BaseUrl}/context", ct) ?? new();

        public async Task<RegisterDokumentResultDto> RegisterAsync(RegisterDokumentRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Rejestracja dokumentu nie powiodła się: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }

            return (await resp.Content.ReadFromJsonAsync<RegisterDokumentResultDto>(cancellationToken: ct))!;
        }
    }
}