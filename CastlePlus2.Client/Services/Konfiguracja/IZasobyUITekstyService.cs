using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Contracts.Requests.Konfiguracja;

namespace CastlePlus2.Client.Services.Konfiguracja;

public interface IZasobyUITekstyService
{
    Task<List<ZasobUITekstDto>> GetByEncjaAsync(Guid idEncji, CancellationToken ct = default);
    Task<ZasobUITekstDto?> GetByZasobIdAsync(long idZasobuTekstu, CancellationToken ct = default);

    Task<long> CreateAsync(CreateZasobUITekstRequest request, CancellationToken ct = default);
    Task<bool> UpdateAsync(long idZasobuTekstu, UpdateZasobUITekstRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(long idZasobuTekstu, CancellationToken ct = default);
}
