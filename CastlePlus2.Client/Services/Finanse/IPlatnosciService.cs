using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;

namespace CastlePlus2.Client.Services.Finanse
{
    public interface IPlatnosciService
    {
        Task<List<PlatnoscDto>> GetAllAsync(CancellationToken ct = default);
        Task<PlatnoscDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<PlatnoscDto> CreateAsync(CreatePlatnoscRequest request, CancellationToken ct = default);
        Task<PlatnoscDto?> UpdateAsync(long id, UpdatePlatnoscRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
