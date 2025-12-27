using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;

namespace CastlePlus2.Client.Services.Rdzen
{
    public interface IEncjeService
    {
        Task<List<EncjaDto>> GetAllAsync(CancellationToken ct = default);
        Task<EncjaDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<EncjaDto> CreateAsync(CreateEncjaRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, UpdateEncjaRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}