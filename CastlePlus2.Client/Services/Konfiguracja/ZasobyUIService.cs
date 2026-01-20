using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Contracts.Requests.Konfiguracja;

namespace CastlePlus2.Client.Services.Konfiguracja;

public class ZasobyUIService(HttpClient http) : IZasobyUIService
{
    private const string Base = "api/konfiguracja/zasobyui";

    public async Task<List<ZasobUIDto>> GetAllAsync(string? typ = null, string? kategoria = null, bool? includeInactive = null, CancellationToken ct = default)
    {
        var qs = new List<string>();
        if (!string.IsNullOrWhiteSpace(typ)) qs.Add($"typ={Uri.EscapeDataString(typ)}");
        if (!string.IsNullOrWhiteSpace(kategoria)) qs.Add($"kategoria={Uri.EscapeDataString(kategoria)}");
        if (includeInactive.HasValue) qs.Add($"includeInactive={includeInactive.Value.ToString().ToLowerInvariant()}");

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
        resp.EnsureSuccessStatusCode();

        // API może zwracać Guid albo cały DTO – obsługujemy oba warianty.
        try
        {
            var id = await resp.Content.ReadFromJsonAsync<Guid>(cancellationToken: ct);
            if (id != Guid.Empty) return id;
        }
        catch { /* ignore */ }

        try
        {
            var dto = await resp.Content.ReadFromJsonAsync<ZasobUIDto>(cancellationToken: ct);
            if (dto is not null && dto.IdEncji != Guid.Empty) return dto.IdEncji;
        }
        catch { /* ignore */ }

        // fallback: Location header
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
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(Guid idEncji, CancellationToken ct = default)
    {
        var resp = await http.DeleteAsync($"{Base}/{idEncji}", ct);
        return resp.IsSuccessStatusCode;
    }
}
