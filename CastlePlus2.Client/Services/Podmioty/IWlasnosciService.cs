using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;

namespace CastlePlus2.Client.Services.Podmioty
{
    public interface IWlasnosciService
    {
        Task<List<WlasnoscDto>> GetAllAsync(CancellationToken ct = default);
        Task<WlasnoscDto?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<List<WlasnoscDto>> GetByEncjaAsync(Guid idEncji, CancellationToken ct = default);
        Task<WlasnoscDto> CreateAsync(CreateWlasnoscRequest request, CancellationToken ct = default);
        Task<WlasnoscDto?> UpdateAsync(long id, UpdateWlasnoscRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}