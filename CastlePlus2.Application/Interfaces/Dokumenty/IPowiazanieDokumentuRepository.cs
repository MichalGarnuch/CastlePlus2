using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Dokumenty;

namespace CastlePlus2.Application.Interfaces.Dokumenty
{
    public interface IPowiazanieDokumentuRepository
    {
        Task<PowiazanieDokumentu?> GetByIdAsync(long idPowiazania, CancellationToken ct);
        Task<bool> ExistsAsync(long idDokumentu, Guid idEncji, CancellationToken ct);

        Task AddAsync(PowiazanieDokumentu entity, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
