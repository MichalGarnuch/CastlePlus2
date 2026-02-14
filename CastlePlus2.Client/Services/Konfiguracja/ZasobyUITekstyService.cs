using System.Linq;
using System.Net.Http.Json;
using CastlePlus2.Client.Services.Common;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Contracts.Requests.Konfiguracja;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Client.Services.Konfiguracja;

public class ZasobyUITekstyService(HttpClient http) : IZasobyUITekstyService
{
    private const string Base = "api/konfiguracja/zasobyuiteksty";

    public async Task<List<ZasobUITekstDto>> GetByEncjaAsync(Guid idEncji, CancellationToken ct = default)
        => (await http.GetFromJsonAsync<List<ZasobUITekstDto>>($"{Base}/by-encja/{idEncji}", ct)) ?? new();

    public async Task<ZasobUITekstDto?> GetByZasobIdAsync(long idZasobuTekstu, CancellationToken ct = default)
        => await http.GetFromJsonAsync<ZasobUITekstDto>($"{Base}/{idZasobuTekstu}", ct);

    public async Task<long> CreateAsync(CreateZasobUITekstRequest request, CancellationToken ct = default)
    {
        var resp = await http.PostAsJsonAsync(Base, request, ct);
        if (!resp.IsSuccessStatusCode)
            await ThrowForErrorAsync(resp, "Nie udało się dodać tekstu.", ct);

        try
        {
            var dto = await resp.Content.ReadFromJsonAsync<ZasobUITekstDto>(cancellationToken: ct);
            if (dto is not null)
                return dto.IdZasobuTekstu;
        }
        catch
        {
            // fallback niżej
        }

        try
        {
            var id = await resp.Content.ReadFromJsonAsync<long>(cancellationToken: ct);
            if (id > 0)
                return id;
        }
        catch
        {
            // fallback niżej
        }

        throw new InvalidOperationException("Nie udało się odczytać IdZasobuTekstu z odpowiedzi API.");
    }

    public async Task<bool> UpdateAsync(long idZasobuTekstu, UpdateZasobUITekstRequest request, CancellationToken ct = default)
    {
        var resp = await http.PutAsJsonAsync($"{Base}/{idZasobuTekstu}", request, ct);
        if (!resp.IsSuccessStatusCode)
            await ThrowForErrorAsync(resp, "Nie udało się zaktualizować tekstu.", ct);

        return true;
    }

    public async Task<bool> DeleteAsync(long idZasobuTekstu, CancellationToken ct = default)
    {
        var resp = await http.DeleteAsync($"{Base}/{idZasobuTekstu}", ct);
        if (!resp.IsSuccessStatusCode)
            await ThrowForErrorAsync(resp, "Nie udało się usunąć tekstu.", ct);

        return true;
    }

    private static async Task ThrowForErrorAsync(HttpResponseMessage response, string fallbackMessage, CancellationToken ct)
    {
        string? message = null;
        IReadOnlyDictionary<string, string[]>? validationErrors = null;

        try
        {
            var validation = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>(cancellationToken: ct);
            if (validation?.Errors?.Count > 0)
            {
                validationErrors = validation.Errors.ToDictionary(e => e.Key, e => e.Value);
                message = string.Join("; ", validation.Errors.SelectMany(x => x.Value).Distinct());
            }
            else if (!string.IsNullOrWhiteSpace(validation?.Detail))
            {
                message = validation.Detail;
            }
        }
        catch
        {
            // ignore
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            try
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken: ct);
                message = !string.IsNullOrWhiteSpace(problem?.Detail)
                    ? problem.Detail
                    : problem?.Title;
            }
            catch
            {
                // ignore
            }
        }

        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest && validationErrors is not null)
        {
            throw new ApiValidationException(message ?? fallbackMessage, validationErrors);
        }

        throw new InvalidOperationException(message ?? fallbackMessage);
    }
}