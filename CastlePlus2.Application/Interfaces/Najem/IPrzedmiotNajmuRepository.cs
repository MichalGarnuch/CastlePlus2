using CastlePlus2.Domain.Entities.Najem;

namespace CastlePlus2.Application.Interfaces.Najem
{
    public interface IPrzedmiotNajmuRepository
    {
        Task AddAsync(PrzedmiotNajmu entity, CancellationToken ct);
        Task<PrzedmiotNajmu?> GetByIdAsync(long id, CancellationToken ct);
        Task<List<PrzedmiotNajmu>> GetAllAsync(CancellationToken ct);
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
