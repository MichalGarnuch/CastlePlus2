using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Rdzen
{
    public class ProcesyRdzenService : IProcesyRdzenService
    {
        private const string BaseUrl = "api/rdzen/procesy/adresy-przypisania";
        private readonly HttpClient _http;
        private readonly ILogger<ProcesyRdzenService> _logger;

        public ProcesyRdzenService(HttpClient http, ILogger<ProcesyRdzenService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<PrzypisanieAdresuContextDto> GetPrzypisanieAdresuContextAsync(CancellationToken ct = default)
            => await _http.GetFromJsonAsync<PrzypisanieAdresuContextDto>($"{BaseUrl}/context", ct) ?? new();

        public async Task<PrzypiszAdresResultDto> PrzypiszAdresAsync(PrzypiszAdresRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync($"{BaseUrl}/przypisz", request, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Przypisanie adresu nie powiodło się: {Status} {Body}", resp.StatusCode, body);
                resp.EnsureSuccessStatusCode();
            }

            return (await resp.Content.ReadFromJsonAsync<PrzypiszAdresResultDto>(cancellationToken: ct))!;
        }
        public async Task<List<EncjaLookupDto>> SearchEncjeLookupAsync(string? typEncji, string? q, int take = 50, CancellationToken ct = default)
        {
            var url =
                $"{BaseUrl}/encje-lookup?typEncji={Uri.EscapeDataString(typEncji ?? string.Empty)}" +
                $"&q={Uri.EscapeDataString(q ?? string.Empty)}" +
                $"&take={take}";

            return await _http.GetFromJsonAsync<List<EncjaLookupDto>>(url, ct) ?? new();
        }

    }
}