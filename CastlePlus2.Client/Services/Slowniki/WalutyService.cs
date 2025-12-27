using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Slowniki;
using CastlePlus2.Contracts.Requests.Slownik;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Slowniki
{
    public class WalutyService : IWalutyService
    {
        private readonly HttpClient _http;
        private readonly ILogger<WalutyService> _logger;

        private const string BaseUrl = "api/slowniki/Waluty";

        public WalutyService(HttpClient http, ILogger<WalutyService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<WalutaDto>> GetAllAsync(CancellationToken ct = default)
            => await _http.GetFromJsonAsync<List<WalutaDto>>(BaseUrl, ct) ?? new();

        public async Task<WalutaDto?> GetByKodAsync(string kodWaluty, CancellationToken ct = default)
        {
            var resp = await _http.GetAsync($"{BaseUrl}/{Uri.EscapeDataString(kodWaluty)}", ct);
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<WalutaDto>(cancellationToken: ct);
        }

        public async Task<WalutaDto> CreateAsync(CreateWalutaRequest request, CancellationToken ct = default)
        {
            try
            {
                var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
                resp.EnsureSuccessStatusCode();
                return (await resp.Content.ReadFromJsonAsync<WalutaDto>(cancellationToken: ct))!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd Create Waluta");
                throw;
            }
        }

        public async Task UpdateAsync(string kodWaluty, UpdateWalutaRequest request, CancellationToken ct = default)
        {
            try
            {
                var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{Uri.EscapeDataString(kodWaluty)}", request, ct);
                resp.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd Update Waluta {KodWaluty}", kodWaluty);
                throw;
            }
        }

        public async Task DeleteAsync(string kodWaluty, CancellationToken ct = default)
        {
            try
            {
                var resp = await _http.DeleteAsync($"{BaseUrl}/{Uri.EscapeDataString(kodWaluty)}", ct);
                resp.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd Delete Waluta {KodWaluty}", kodWaluty);
                throw;
            }
        }
    }
}
