using CastlePlus2.Domain.Entities.Slowniki;

namespace CastlePlus2.Application.Interfaces.Slowniki
{
    public interface IWalutaRepository
    {
        Task AddAsync(Waluta waluta, CancellationToken ct);
        Task<Waluta?> GetByKodAsync(string kodWaluty, CancellationToken ct);
        Task<List<Waluta>> GetAllAsync(CancellationToken ct);
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
