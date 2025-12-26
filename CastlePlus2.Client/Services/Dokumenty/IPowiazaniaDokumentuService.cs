using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Contracts.Requests.Dokumenty;

namespace CastlePlus2.Client.Services.Dokumenty
{
    public interface IPowiazaniaDokumentuService
    {
        Task<List<PowiazanieDokumentuDto>> GetAllAsync(CancellationToken ct = default);
        Task<PowiazanieDokumentuDto?> GetByIdAsync(long idPowiazania, CancellationToken ct = default);

        Task<PowiazanieDokumentuDto> CreateAsync(CreatePowiazanieDokumentuRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(long idPowiazania, UpdatePowiazanieDokumentuRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long idPowiazania, CancellationToken ct = default);
    }
}
