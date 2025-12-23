using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;

namespace CastlePlus2.Client.Services.Finanse
{
    public interface IAlokacjeKosztowService
    {
        Task<List<AlokacjaKosztuDto>> GetAllAsync(CancellationToken ct = default);
        Task<AlokacjaKosztuDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<AlokacjaKosztuDto> CreateAsync(CreateAlokacjaKosztuRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(long id, UpdateAlokacjaKosztuRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
