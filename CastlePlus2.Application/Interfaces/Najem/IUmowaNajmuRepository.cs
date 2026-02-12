using CastlePlus2.Domain.Entities.Najem;

namespace CastlePlus2.Application.Interfaces.Najem
{
    public interface IUmowaNajmuRepository
    {
        Task AddAsync(UmowaNajmu entity, CancellationToken ct);
        Task<UmowaNajmu?> GetByIdAsync(Guid idEncji, CancellationToken ct);
        Task<List<UmowaNajmu>> GetAllAsync(CancellationToken ct);
        Task<UmowaNajmu?> GetForUpdateAsync(Guid idEncji, CancellationToken ct);
        void Remove(UmowaNajmu entity);
        Task<int> SaveChangesAsync(CancellationToken ct);

        // Lookup / paged search (dla modal lookup)
        Task<(List<UmowaNajmu> Items, int TotalCount)> SearchPagedAsync(
            string? q,
            int page,
            int pageSize,
            CancellationToken ct);
    }
}
