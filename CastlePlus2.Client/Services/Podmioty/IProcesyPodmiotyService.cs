using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;

namespace CastlePlus2.Client.Services.Podmioty
{
    public interface IProcesyPodmiotyService
    {
        Task<WlasnoscContextDto> GetWlasnoscContextAsync(CancellationToken ct = default);
        Task<IReadOnlyList<WlasnoscDto>> UstawWlasnoscAsync(UstawWlasnoscRequest request, CancellationToken ct = default);
    }
}