using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Contracts.Requests.Dokumenty;

namespace CastlePlus2.Client.Services.Dokumenty
{
    public interface IDokumentyService
    {
        Task<List<DokumentDto>> GetAllAsync(CancellationToken ct = default);
        Task<DokumentDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<DokumentDto> CreateAsync(CreateDokumentRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(long id, UpdateDokumentRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
