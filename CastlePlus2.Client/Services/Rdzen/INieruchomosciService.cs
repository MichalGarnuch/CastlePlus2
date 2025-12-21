using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;

namespace CastlePlus2.Client.Services.Rdzen
{
    public interface INieruchomosciService
    {
        Task<List<NieruchomoscDto>> GetAllAsync(CancellationToken ct = default);
        Task<NieruchomoscDto?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<Guid> CreateAsync(CreateNieruchomoscRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, UpdateNieruchomoscRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
