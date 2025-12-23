using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Finanse;

namespace CastlePlus2.Application.Interfaces.Finanse
{
    public interface IPlatnoscRepository
    {
        Task<List<Platnosc>> GetAllAsync(CancellationToken ct);
        Task<Platnosc?> GetByIdAsync(long id, CancellationToken ct);
        Task<Platnosc?> GetForUpdateAsync(long id, CancellationToken ct);

        Task<bool> PodmiotExistsAsync(long idPodmiotu, CancellationToken ct);
        Task<bool> WalutaExistsAsync(string kodWaluty, CancellationToken ct);

        Task AddAsync(Platnosc entity, CancellationToken ct);
        void Remove(Platnosc entity);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
