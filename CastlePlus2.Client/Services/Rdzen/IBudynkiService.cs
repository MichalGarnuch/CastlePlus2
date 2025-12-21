using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;

namespace CastlePlus2.Client.Services.Rdzen
{
    public interface IBudynkiService
    {
        Task<List<BudynekDto>> GetAllAsync(CancellationToken ct = default);
        Task<BudynekDto?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<Guid> CreateAsync(CreateBudynekRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, UpdateBudynekRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
