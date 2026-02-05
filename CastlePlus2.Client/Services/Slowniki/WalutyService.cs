using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CastlePlus2.Client.Services.Common;
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
                if (resp.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errors = await TryReadValidationErrorsAsync(resp, ct);
                    if (errors is not null)
                        throw new ApiValidationException("Błąd walidacji danych waluty.", errors);
                }

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
                if (resp.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errors = await TryReadValidationErrorsAsync(resp, ct);
                    if (errors is not null)
                        throw new ApiValidationException("Błąd walidacji danych waluty.", errors);
                }

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

        private static async Task<IReadOnlyDictionary<string, string[]>?> TryReadValidationErrorsAsync(
            HttpResponseMessage response,
            CancellationToken ct)
        {
            try
            {
                var json = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: ct);
                if (json.ValueKind != JsonValueKind.Object)
                    return null;

                if (json.TryGetProperty("errors", out var errors) && errors.ValueKind == JsonValueKind.Object)
                {
                    var dict = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

                    foreach (var field in errors.EnumerateObject())
                    {
                        if (field.Value.ValueKind != JsonValueKind.Array)
                            continue;

                        var messages = field.Value.EnumerateArray()
                            .Select(x => x.GetString())
                            .Where(x => !string.IsNullOrWhiteSpace(x))
                            .Select(x => x!)
                            .ToArray();

                        if (messages.Length > 0)
                            dict[field.Name] = messages;
                    }

                    return dict.Count > 0 ? dict : null;
                }
            }
            catch (JsonException) { }
            catch (NotSupportedException) { }

            return null;
        }
    }
}