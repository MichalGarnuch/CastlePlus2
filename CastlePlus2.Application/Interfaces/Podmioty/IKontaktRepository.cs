using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Podmioty;

namespace CastlePlus2.Application.Interfaces.Podmioty
{
    public interface IKontaktRepository
    {
        Task<Kontakt?> GetByIdAsync(long idKontaktu, CancellationToken ct);
        Task<List<Kontakt>> GetByPodmiotIdAsync(long idPodmiotu, CancellationToken ct);

        Task<bool> PodmiotExistsAsync(long idPodmiotu, CancellationToken ct);

        Task AddAsync(Kontakt kontakt, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
