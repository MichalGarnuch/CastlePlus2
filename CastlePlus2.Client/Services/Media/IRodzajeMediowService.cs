using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Media;

namespace CastlePlus2.Client.Services.Media
{
    public interface IRodzajeMediowService
    {
        Task<List<RodzajMediumDto>> GetAllAsync(CancellationToken ct = default);
        Task<RodzajMediumDto?> GetByIdAsync(string kodRodzaju, CancellationToken ct = default);
        Task<RodzajMediumDto> CreateAsync(CreateRodzajMediumRequest request, CancellationToken ct = default);
        Task<RodzajMediumDto?> UpdateAsync(string kodRodzaju, UpdateRodzajMediumRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(string kodRodzaju, CancellationToken ct = default);
    }
}
