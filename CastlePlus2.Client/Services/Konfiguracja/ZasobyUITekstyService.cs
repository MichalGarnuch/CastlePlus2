using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Contracts.Requests.Konfiguracja;

namespace CastlePlus2.Client.Services.Konfiguracja;

public class ZasobyUITekstyService(HttpClient http) : IZasobyUITekstyService
{
    private const string Base = "api/konfiguracja/zasobyuiteksty";

    // FIX: endpoint w API to "by-encja", a nie "encja"
    public async Task<List<ZasobUITekstDto>> GetByEncjaAsync(Guid idEncji, CancellationToken ct = default)
    => (await http.GetFromJsonAsync<List<ZasobUITekstDto>>($"{Base}/by-encja/{idEncji}", ct)) ?? new();

    public async Task<ZasobUITekstDto?> GetByZasobIdAsync(long idZasobuTekstu, CancellationToken ct = default)
        => await http.GetFromJsonAsync<ZasobUITekstDto>($"{Base}/{idZasobuTekstu}", ct);

    public async Task<long> CreateAsync(CreateZasobUITekstRequest request, CancellationToken ct = default)
    {
        var resp = await http.PostAsJsonAsync(Base, request, ct);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<long>(cancellationToken: ct);
    }

    public async Task<bool> UpdateAsync(long idZasobuTekstu, UpdateZasobUITekstRequest request, CancellationToken ct = default)
    {
        var resp = await http.PutAsJsonAsync($"{Base}/{idZasobuTekstu}", request, ct);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(long idZasobuTekstu, CancellationToken ct = default)
    {
        var resp = await http.DeleteAsync($"{Base}/{idZasobuTekstu}", ct);
        return resp.IsSuccessStatusCode;
    }
}
