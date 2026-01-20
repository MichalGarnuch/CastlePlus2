using CastlePlus2.Domain.Entities.Konfiguracja;

namespace CastlePlus2.Application.Interfaces.Konfiguracja
{
    public interface IZasobUIRepository
    {
        Task<ZasobUI?> GetByIdAsync(Guid idEncji, CancellationToken ct);
        Task<ZasobUI?> GetForUpdateAsync(Guid idEncji, CancellationToken ct);
        Task<ZasobUI?> GetByKodZasobuAsync(string kodZasobu, CancellationToken ct);
        Task<List<ZasobUI>> GetAllAsync(string? typ, string? kategoria, bool? czyAktywny, CancellationToken ct);
        Task<List<ZasobUI>> GetPublicAsync(string typ, string? kategoria, bool includeInactive, DateTime nowUtc, CancellationToken ct);
        Task AddAsync(ZasobUI entity, CancellationToken ct);
        void Remove(ZasobUI entity);
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}