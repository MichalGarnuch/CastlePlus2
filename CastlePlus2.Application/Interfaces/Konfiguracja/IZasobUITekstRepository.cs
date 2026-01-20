using CastlePlus2.Domain.Entities.Konfiguracja;

namespace CastlePlus2.Application.Interfaces.Konfiguracja
{
    public interface IZasobUITekstRepository
    {
        Task<ZasobUITekst?> GetByIdAsync(long idZasobuTekstu, CancellationToken ct);
        Task<ZasobUITekst?> GetForUpdateAsync(long idZasobuTekstu, CancellationToken ct);
        Task<ZasobUITekst?> GetByKeyAsync(Guid idEncji, string jezyk, string pole, CancellationToken ct);
        Task<List<ZasobUITekst>> GetByEncjaIdAsync(Guid idEncji, CancellationToken ct);
        Task AddAsync(ZasobUITekst entity, CancellationToken ct);
        void Remove(ZasobUITekst entity);
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}