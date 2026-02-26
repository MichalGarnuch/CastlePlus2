using CastlePlus2.Domain.Entities.Dokumenty;

namespace CastlePlus2.Application.Interfaces.Dokumenty
{
    public interface IPowiazanieDokumentuRepository
    {
        Task<PowiazanieDokumentu?> GetByIdAsync(long idPowiazania, CancellationToken ct);
        Task<PowiazanieDokumentu?> GetForUpdateAsync(long idPowiazania, CancellationToken ct);
        Task<List<PowiazanieDokumentu>> GetAllAsync(CancellationToken ct);
        Task<List<PowiazanieDokumentu>> GetByEncjaIdAsync(Guid idEncji, CancellationToken ct);
        Task<List<Dokument>> GetDokumentyByEncjaIdAsync(Guid idEncji, CancellationToken ct);
        Task<Dictionary<Guid, List<Dokument>>> GetDokumentyByEncjeIdsAsync(List<Guid> idEncjiList, CancellationToken ct);

        Task<bool> ExistsAsync(long idDokumentu, Guid idEncji, CancellationToken ct);
        Task<bool> ExistsOtherAsync(long idDokumentu, Guid idEncji, long excludeIdPowiazania, CancellationToken ct);

        Task AddAsync(PowiazanieDokumentu entity, CancellationToken ct);
        Task RemoveAsync(PowiazanieDokumentu entity, CancellationToken ct);

        Task SaveChangesAsync(CancellationToken ct);
    }
}
