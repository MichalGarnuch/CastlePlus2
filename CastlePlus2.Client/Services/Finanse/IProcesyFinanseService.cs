using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;

namespace CastlePlus2.Client.Services.Finanse
{
    public interface IProcesyFinanseService
    {
        Task<WystawFaktureContextDto> GetWystawFaktureContextAsync(CancellationToken ct = default);
        Task<WystawFaktureResultDto> WystawFaktureAsync(WystawFaktureRequest request, CancellationToken ct = default);
    }
}