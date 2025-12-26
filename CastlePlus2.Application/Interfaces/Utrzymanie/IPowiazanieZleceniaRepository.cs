using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Utrzymanie;

namespace CastlePlus2.Application.Interfaces.Utrzymanie
{
    public interface IPowiazanieZleceniaRepository
    {
        Task<List<PowiazanieZlecenia>> GetAllAsync(CancellationToken ct);
        Task<PowiazanieZlecenia?> GetByIdAsync(long idPowiazania, CancellationToken ct);
        Task<bool> ExistsAsync(long idZlecenia, Guid idEncji, CancellationToken ct);

        Task AddAsync(PowiazanieZlecenia entity, CancellationToken ct);
        void Remove(PowiazanieZlecenia entity);
        Task SaveChangesAsync(CancellationToken ct);
    }
}