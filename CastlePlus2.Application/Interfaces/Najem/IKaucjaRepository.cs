using CastlePlus2.Domain.Entities.Najem;

namespace CastlePlus2.Application.Interfaces.Najem
{
    public interface IKaucjaRepository
    {
        Task AddAsync(Kaucja entity, CancellationToken ct);
        Task<Kaucja?> GetByIdAsync(long id, CancellationToken ct);
        Task<List<Kaucja>> GetAllAsync(CancellationToken ct);
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
