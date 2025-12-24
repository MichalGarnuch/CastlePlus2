using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;

namespace CastlePlus2.Client.Services.Najem
{
    public interface ISkladnikiCzynszuService
    {
        Task<List<SkladnikCzynszuDto>> GetAllAsync(CancellationToken ct = default);
        Task<SkladnikCzynszuDto?> GetByIdAsync(long idSkladnikaCzynszu, CancellationToken ct = default);

        Task<SkladnikCzynszuDto> CreateAsync(CreateSkladnikCzynszuRequest request, CancellationToken ct = default);
        Task<SkladnikCzynszuDto?> UpdateAsync(long idSkladnikaCzynszu, UpdateSkladnikCzynszuRequest request, CancellationToken ct = default);

        Task<bool> DeleteAsync(long idSkladnikaCzynszu, CancellationToken ct = default);
    }
}