using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;

namespace CastlePlus2.Client.Services.Finanse
{
    public interface IFakturyService
    {
        Task<List<FakturaDto>> GetAllAsync(CancellationToken ct = default);
        Task<FakturaDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<FakturaDto> CreateAsync(CreateFakturaRequest request, CancellationToken ct = default);
        Task<FakturaDto?> UpdateAsync(long id, UpdateFakturaRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
