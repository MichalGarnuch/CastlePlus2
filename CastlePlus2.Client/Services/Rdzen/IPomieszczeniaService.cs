using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;

namespace CastlePlus2.Client.Services.Rdzen
{
    public interface IPomieszczeniaService
    {
        Task<List<PomieszczenieDto>> GetAllAsync(CancellationToken ct = default);
        Task<PomieszczenieDto?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<Guid> CreateAsync(CreatePomieszczenieRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, UpdatePomieszczenieRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
