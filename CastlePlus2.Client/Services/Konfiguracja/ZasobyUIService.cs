using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Contracts.Requests.Konfiguracja;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Client.Services.Konfiguracja;

public class ZasobyUIService(HttpClient http) : IZasobyUIService
{
    private const string Base = "api/konfiguracja/zasobyui";

    public async Task<List<ZasobUIDto>> GetAllAsync(string? typ = null, string? kategoria = null, bool? aktywny = null, CancellationToken ct = default)
    {
        var qs = new List<string>();
        if (!string.IsNullOrWhiteSpace(typ)) qs.Add($"typ={Uri.EscapeDataString(typ)}");
        if (!string.IsNullOrWhiteSpace(kategoria)) qs.Add($"kategoria={Uri.EscapeDataString(kategoria)}");
        if (aktywny.HasValue) qs.Add($"aktywny={aktywny.Value.ToString().ToLowerInvariant()}");

        var url = qs.Count == 0 ? Base : $"{Base}?{string.Join("&", qs)}";
        return (await http.GetFromJsonAsync<List<ZasobUIDto>>(url, ct)) ?? new();
    }

    public async Task<ZasobUIDto?> GetByIdAsync(Guid idEncji, CancellationToken ct = default)
        => await http.GetFromJsonAsync<ZasobUIDto>($"{Base}/{idEncji}", ct);

    public async Task<List<ZasobUIPublicDto>> GetPublicAsync(string typ, string? kategoria = null, string? jezyk = null, bool includeInactive = false, CancellationToken ct = default)
    {
        var qs = new List<string> { $"typ={Uri.EscapeDataString(typ)}" };
        if (!string.IsNullOrWhiteSpace(kategoria)) qs.Add($"kategoria={Uri.EscapeDataString(kategoria)}");
        if (!string.IsNullOrWhiteSpace(jezyk)) qs.Add($"jezyk={Uri.EscapeDataString(jezyk)}");
        qs.Add($"includeInactive={includeInactive.ToString().ToLowerInvariant()}");

        var url = $"{Base}/public?{string.Join("&", qs)}";
        return (await http.GetFromJsonAsync<List<ZasobUIPublicDto>>(url, ct)) ?? new();
    }

    public async Task<Guid> CreateAsync(CreateZasobUIRequest request, CancellationToken ct = default)
    {
        var resp = await http.PostAsJsonAsync(Base, request, ct);
        if (!resp.IsSuccessStatusCode)
            await ThrowForErrorAsync(resp, "Nie udało się utworzyć zasobu UI.", ct);

        try
        {
            var id = await resp.Content.ReadFromJsonAsync<Guid>(cancellationToken: ct);
            if (id != Guid.Empty) return id;
        }
        catch { }

        try
        {
            var dto = await resp.Content.ReadFromJsonAsync<ZasobUIDto>(cancellationToken: ct);
            if (dto is not null && dto.IdEncji != Guid.Empty) return dto.IdEncji;
        }
        catch { }

        if (resp.Headers.Location is not null)
        {
            var last = resp.Headers.Location.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            if (Guid.TryParse(last, out var parsed)) return parsed;
        }

        throw new InvalidOperationException("Nie udało się odczytać IdEncji z odpowiedzi API (Create ZasobUI).");
    }

    public async Task<bool> UpdateAsync(Guid idEncji, UpdateZasobUIRequest request, CancellationToken ct = default)
    {
        var resp = await http.PutAsJsonAsync($"{Base}/{idEncji}", request, ct);
        if (!resp.IsSuccessStatusCode)
            await ThrowForErrorAsync(resp, "Nie udało się zapisać zmian zasobu UI.", ct);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid idEncji, CancellationToken ct = default)
    {
        var resp = await http.DeleteAsync($"{Base}/{idEncji}", ct);
        return resp.IsSuccessStatusCode;
    }

    private static async Task ThrowForErrorAsync(HttpResponseMessage response, string fallbackMessage, CancellationToken ct)
    {
        string? message = null;

        try
        {
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken: ct);
            if (!string.IsNullOrWhiteSpace(problem?.Detail)) message = problem.Detail;
            else if (!string.IsNullOrWhiteSpace(problem?.Title)) message = problem.Title;
        }
        catch { }

        message ??= fallbackMessage;
        throw new InvalidOperationException(message);
    }
}
