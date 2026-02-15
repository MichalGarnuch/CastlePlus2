using CastlePlus2.Domain.Entities.Najem;

namespace CastlePlus2.Application.Interfaces.Najem
{
    public interface ISkladnikCzynszuRepository
    {
        Task AddAsync(SkladnikCzynszu entity, CancellationToken ct);
        Task<SkladnikCzynszu?> GetByIdAsync(long id, CancellationToken ct);
        Task<List<SkladnikCzynszu>> GetAllAsync(CancellationToken ct);
        Task<SkladnikCzynszu?> GetActiveByNameAsync(Guid idUmowyNajmu, string nazwa, DateOnly odDnia, CancellationToken ct);
        Task<bool> ExistsOverlapAsync(Guid idUmowyNajmu, string nazwa, DateOnly odDnia, long? excludeId, CancellationToken ct);
        Task<List<SkladnikCzynszu>> GetOpenForUpdateByUmowaIdAsync(Guid idUmowyNajmu, DateOnly dataZakonczenia, CancellationToken ct);
        Task<List<SkladnikCzynszu>> GetActiveInRangeByUmowaIdAsync(Guid idUmowyNajmu, DateOnly from, DateOnly to, CancellationToken ct);
        void Remove(SkladnikCzynszu entity);
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
