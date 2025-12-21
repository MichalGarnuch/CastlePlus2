using CastlePlus2.Contracts.DTOs.Slowniki;

namespace CastlePlus2.Client.Services.Slowniki
{
    public interface IWalutyService
    {
        Task<List<WalutaDto>> GetAllAsync(CancellationToken ct = default);
        Task<WalutaDto?> GetByKodAsync(string kodWaluty, CancellationToken ct = default);
        Task<WalutaDto> CreateAsync(CreateWalutaRequest request, CancellationToken ct = default);
        Task UpdateAsync(string kodWaluty, UpdateWalutaRequest request, CancellationToken ct = default);
        Task DeleteAsync(string kodWaluty, CancellationToken ct = default);
    }
}
