using CastlePlus2.Domain.Entities.Najem;

namespace CastlePlus2.Application.Interfaces.Najem
{
    public interface IUmowaNajmuRepository
    {
        Task AddAsync(UmowaNajmu entity, CancellationToken ct);
        Task<UmowaNajmu?> GetByIdAsync(Guid idEncji, CancellationToken ct);
        Task<List<UmowaNajmu>> GetAllAsync(CancellationToken ct);
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
