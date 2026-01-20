using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Contracts.Requests.Dokumenty;

namespace CastlePlus2.Client.Services.Dokumenty;

public interface IDokumentyService
{
    Task<List<DokumentDto>> GetAllAsync(CancellationToken ct = default);
    Task<DokumentDto?> GetByIdAsync(long idDokumentu, CancellationToken ct = default);
    Task<DokumentDto> CreateAsync(CreateDokumentRequest request, CancellationToken ct = default);

    // ZMIANA: Task -> Task<bool> (false gdy 404)
    Task<bool> UpdateAsync(long idDokumentu, UpdateDokumentRequest request, CancellationToken ct = default);

    // ZMIANA: Task -> Task<bool> (false gdy 404)
    Task<bool> DeleteAsync(long idDokumentu, CancellationToken ct = default);

    Task<List<DokumentDto>> GetByEncjaIdAsync(Guid idEncji, CancellationToken ct = default);
    Task<byte[]> DownloadAsync(long idDokumentu, CancellationToken ct = default);
}
