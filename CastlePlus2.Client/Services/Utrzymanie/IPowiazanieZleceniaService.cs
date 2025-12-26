using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Contracts.Requests.Utrzymanie;

namespace CastlePlus2.Client.Services.Utrzymanie
{
    public interface IPowiazaniaZleceniaService
    {
        Task<List<PowiazanieZleceniaDto>> GetAllAsync(CancellationToken ct = default);
        Task<PowiazanieZleceniaDto?> GetByIdAsync(long idPowiazania, CancellationToken ct = default);
        Task<PowiazanieZleceniaDto> CreateAsync(CreatePowiazanieZleceniaRequest request, CancellationToken ct = default);
        Task<PowiazanieZleceniaDto?> UpdateAsync(long idPowiazania, UpdatePowiazanieZleceniaRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long idPowiazania, CancellationToken ct = default);
    }
}