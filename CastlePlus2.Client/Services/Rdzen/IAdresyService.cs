using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;

namespace CastlePlus2.Client.Services.Rdzen
{
    public interface IAdresyService
    {
        Task<List<AdresDto>> GetAllAsync(CancellationToken ct = default);
        Task<AdresDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<long> CreateAsync(CreateAdresRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(long id, UpdateAdresRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
