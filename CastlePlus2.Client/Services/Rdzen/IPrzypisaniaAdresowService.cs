using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;

namespace CastlePlus2.Client.Services.Rdzen
{
    public interface IPrzypisaniaAdresowService
    {
        Task<List<PrzypisanieAdresuDto>> GetAllAsync(CancellationToken ct = default);
        Task<PrzypisanieAdresuDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<long> CreateAsync(CreatePrzypisanieAdresuRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(long id, UpdatePrzypisanieAdresuRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
