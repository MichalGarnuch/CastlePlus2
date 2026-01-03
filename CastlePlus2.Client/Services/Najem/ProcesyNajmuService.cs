using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Najem
{
    public class ProcesyNajmuService : IProcesyNajmuService
    {
        private readonly HttpClient _http;
        private readonly ILogger<ProcesyNajmuService> _logger;
        private const string BaseUrl = "api/najem/procesy";

        public ProcesyNajmuService(HttpClient http, ILogger<ProcesyNajmuService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<AneksujCzynszResult> AneksujCzynszAsync(Guid idUmowyNajmu, AneksujCzynszRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync($"{BaseUrl}/umowy/{idUmowyNajmu}/czynsz/aneks", request, ct);
            if (resp.IsSuccessStatusCode)
            {
                return (await resp.Content.ReadFromJsonAsync<AneksujCzynszResult>(cancellationToken: ct))!;
            }

            var msg =
                await TryReadValidationMessageAsync(resp, ct)
                ?? await TryReadMessageAsync(resp, ct)
                ?? await TryReadRawBodyAsync(resp, ct)
                ?? $"{(int)resp.StatusCode} {resp.ReasonPhrase}";

            _logger.LogWarning("Aneks czynszu nie powiódł się: {Status} {Message}", resp.StatusCode, msg);

            return new AneksujCzynszResult
            {
                IdUmowyNajmu = idUmowyNajmu,
                Message = msg
            };
        }

        private static async Task<string?> TryReadValidationMessageAsync(HttpResponseMessage response, CancellationToken ct)
        {
            try
            {
                var json = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: ct);
                if (json.ValueKind != JsonValueKind.Object)
                    return null;

                if (json.TryGetProperty("errors", out var errors) && errors.ValueKind == JsonValueKind.Object)
                {
                    var sb = new StringBuilder();

                    foreach (var field in errors.EnumerateObject())
                    {
                        if (field.Value.ValueKind != JsonValueKind.Array)
                            continue;

                        foreach (var msg in field.Value.EnumerateArray())
                        {
                            var text = msg.GetString();
                            if (!string.IsNullOrWhiteSpace(text))
                                sb.AppendLine(text);
                        }
                    }

                    return sb.Length == 0 ? null : sb.ToString().Trim();
                }
            }
            catch
            {
                // ignorujemy
            }

            return null;
        }

        private static async Task<string?> TryReadMessageAsync(HttpResponseMessage response, CancellationToken ct)
        {
            try
            {
                var json = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: ct);
                if (json.ValueKind == JsonValueKind.Object && json.TryGetProperty("message", out var msg))
                    return msg.GetString();
            }
            catch
            {
                // ignorujemy
            }

            return null;
        }

        private static async Task<string?> TryReadRawBodyAsync(HttpResponseMessage response, CancellationToken ct)
        {
            try
            {
                var body = await response.Content.ReadAsStringAsync(ct);
                return string.IsNullOrWhiteSpace(body) ? null : body;
            }
            catch
            {
                return null;
            }
        }
    }
}