// PLIK: CastlePlus2.Application/Interfaces/Dokumenty/IPowiazanieDokumentuRepository.cs
using CastlePlus2.Domain.Entities.Dokumenty;

namespace CastlePlus2.Application.Interfaces.Dokumenty
{
    public interface IPowiazanieDokumentuRepository
    {
        Task<PowiazanieDokumentu?> GetByIdAsync(long idPowiazania, CancellationToken ct);
        Task<PowiazanieDokumentu?> GetForUpdateAsync(long idPowiazania, CancellationToken ct);
        Task<List<PowiazanieDokumentu>> GetAllAsync(CancellationToken ct);

        Task<bool> ExistsAsync(long idDokumentu, Guid idEncji, CancellationToken ct);
        Task<bool> ExistsOtherAsync(long idDokumentu, Guid idEncji, long excludeIdPowiazania, CancellationToken ct);

        Task AddAsync(PowiazanieDokumentu entity, CancellationToken ct);
        Task RemoveAsync(PowiazanieDokumentu entity, CancellationToken ct);

        Task SaveChangesAsync(CancellationToken ct);
    }
}
