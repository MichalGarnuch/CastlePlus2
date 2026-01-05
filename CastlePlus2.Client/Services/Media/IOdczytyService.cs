using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Contracts.Requests.Media;

namespace CastlePlus2.Client.Services.Media
{
    public interface IOdczytyService
    {
        Task<OdczytContextDto> GetContextAsync(CancellationToken ct = default);
        Task<List<OdczytDto>> GetAllAsync(CancellationToken ct = default);
        Task<OdczytDto?> GetByIdAsync(long idOdczytu, CancellationToken ct = default);

        Task<OdczytDto> CreateAsync(CreateOdczytRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(long idOdczytu, UpdateOdczytRequest request, CancellationToken ct = default);

        Task<bool> DeleteAsync(long idOdczytu, CancellationToken ct = default);
    }
}
