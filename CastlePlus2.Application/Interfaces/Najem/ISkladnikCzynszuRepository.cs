using CastlePlus2.Domain.Entities.Najem;

namespace CastlePlus2.Application.Interfaces.Najem
{
    public interface ISkladnikCzynszuRepository
    {
        Task AddAsync(SkladnikCzynszu entity, CancellationToken ct);
        Task<SkladnikCzynszu?> GetByIdAsync(long id, CancellationToken ct);
        Task<List<SkladnikCzynszu>> GetAllAsync(CancellationToken ct);
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
