using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Contracts.Requests.Media;

namespace CastlePlus2.Client.Services.Media
{
    public interface IPrzylaczaService
    {
        Task<List<PrzylaczeDto>> GetAllAsync(CancellationToken ct = default);
        Task<PrzylaczeDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<PrzylaczeDto> CreateAsync(CreatePrzylaczeRequest request, CancellationToken ct = default);
        Task<PrzylaczeDto?> UpdateAsync(long id, UpdatePrzylaczeRequest request, CancellationToken ct = default);

        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
