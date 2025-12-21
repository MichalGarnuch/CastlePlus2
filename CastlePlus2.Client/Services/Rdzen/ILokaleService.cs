using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;

namespace CastlePlus2.Client.Services.Rdzen
{
    public interface ILokaleService
    {
        Task<List<LokalDto>> GetAllAsync(CancellationToken ct = default);
        Task<LokalDto?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<Guid> CreateAsync(CreateLokalRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, UpdateLokalRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
