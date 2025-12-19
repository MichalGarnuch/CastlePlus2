using CastlePlus2.Domain.Entities.Slowniki;

namespace CastlePlus2.Application.Interfaces.Slowniki
{
    public interface IIndeksacjaRepository
    {
        Task AddAsync(Indeksacja entity, CancellationToken ct);
        Task RemoveAsync(Indeksacja entity, CancellationToken ct);
        Task<Indeksacja?> GetByKodAsync(string kod, CancellationToken ct);
        Task<List<Indeksacja>> GetAllAsync(CancellationToken ct);
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
