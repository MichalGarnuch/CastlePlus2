using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Contracts.Requests.Media;

namespace CastlePlus2.Client.Services.Media
{
    public interface ILicznikiService
    {
        Task<List<LicznikDto>> GetAllAsync(CancellationToken ct = default);
        Task<LicznikDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<LicznikDto> CreateAsync(CreateLicznikRequest request, CancellationToken ct = default);
        Task<LicznikDto?> UpdateAsync(long id, UpdateLicznikRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
