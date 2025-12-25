using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Contracts.Requests.Utrzymanie;

namespace CastlePlus2.Client.Services.Utrzymanie
{
    public interface IZleceniaPracyService
    {
        Task<List<ZleceniePracyDto>> GetAllAsync(CancellationToken ct = default);
        Task<ZleceniePracyDto?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<ZleceniePracyDto> CreateAsync(CreateZleceniePracyRequest request, CancellationToken ct = default);
        Task<ZleceniePracyDto?> UpdateAsync(long id, UpdateZleceniePracyRequest request, CancellationToken ct = default);
        Task DeleteAsync(long id, CancellationToken ct = default);
    }
}