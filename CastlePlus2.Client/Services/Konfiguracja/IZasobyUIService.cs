using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Contracts.Requests.Konfiguracja;

namespace CastlePlus2.Client.Services.Konfiguracja;

public interface IZasobyUIService
{
    Task<List<ZasobUIDto>> GetAllAsync(string? typ = null, string? kategoria = null, bool? aktywny = null, CancellationToken ct = default);
    Task<ZasobUIDto?> GetByIdAsync(Guid idEncji, CancellationToken ct = default);

    Task<List<ZasobUIPublicDto>> GetPublicAsync(
        string typ,
        string? kategoria = null,
        string? jezyk = null,
        bool includeInactive = false,
        CancellationToken ct = default);

    Task<Guid> CreateAsync(CreateZasobUIRequest request, CancellationToken ct = default);
    Task<bool> UpdateAsync(Guid idEncji, UpdateZasobUIRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid idEncji, CancellationToken ct = default);
}
