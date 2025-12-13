using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Utrzymanie;

namespace CastlePlus2.Application.Interfaces.Utrzymanie
{
    public interface IPowiazanieZleceniaRepository
    {
        Task<PowiazanieZlecenia?> GetByIdAsync(long idPowiazania, CancellationToken ct);
        Task<bool> ExistsAsync(long idZlecenia, Guid idEncji, CancellationToken ct);

        Task AddAsync(PowiazanieZlecenia entity, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
