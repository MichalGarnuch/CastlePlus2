using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;

namespace CastlePlus2.Client.Services.Finanse
{
    public interface IRozliczeniaPlatnosciService
    {
        Task<List<RozliczeniePlatnosciDto>> GetAllAsync(CancellationToken ct = default);
        Task<RozliczeniePlatnosciDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<RozliczeniePlatnosciDto> CreateAsync(CreateRozliczeniePlatnosciRequest request, CancellationToken ct = default);
        Task<RozliczeniePlatnosciDto?> UpdateAsync(long id, UpdateRozliczeniePlatnosciRequest request, CancellationToken ct = default);
        Task DeleteAsync(long id, CancellationToken ct = default);
    }
}
