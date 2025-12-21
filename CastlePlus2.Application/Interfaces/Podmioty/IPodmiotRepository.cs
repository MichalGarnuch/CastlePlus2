using CastlePlus2.Domain.Entities.Podmioty;

namespace CastlePlus2.Application.Interfaces.Podmioty
{
    public interface IPodmiotRepository
    {
        Task AddAsync(Podmiot podmiot, CancellationToken ct);
        Task<Podmiot?> GetByIdAsync(long idPodmiotu, CancellationToken ct);
        Task<List<Podmiot>> GetAllAsync(CancellationToken ct);
        void Remove(Podmiot podmiot);

        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
