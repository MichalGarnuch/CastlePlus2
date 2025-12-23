using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Podmioty;

namespace CastlePlus2.Application.Interfaces.Podmioty
{
    public interface IWlasnoscRepository
    {
        Task<Wlasnosc?> GetByIdAsync(long idWlasnosci, CancellationToken ct);
        Task<IReadOnlyList<Wlasnosc>> GetByEncjaIdAsync(Guid idEncji, CancellationToken ct);
        Task<List<Wlasnosc>> GetAllAsync(CancellationToken ct);
        Task<Wlasnosc?> GetForUpdateAsync(long idWlasnosci, CancellationToken ct);

        Task AddAsync(Wlasnosc entity, CancellationToken ct);
        void Remove(Wlasnosc entity);
        Task SaveChangesAsync(CancellationToken ct);

        Task<bool> EncjaExistsAsync(Guid idEncji, CancellationToken ct);
        Task<bool> PodmiotExistsAsync(long idPodmiotu, CancellationToken ct);
    }
}