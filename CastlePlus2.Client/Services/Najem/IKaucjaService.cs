using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;

namespace CastlePlus2.Client.Services.Najem
{
    public interface IKaucjeService
    {
        Task<List<KaucjaDto>> GetAllAsync(CancellationToken ct = default);
        Task<KaucjaDto?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<KaucjaDto> CreateAsync(CreateKaucjaRequest request, CancellationToken ct = default);
        Task<KaucjaDto?> UpdateAsync(long id, UpdateKaucjaRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}