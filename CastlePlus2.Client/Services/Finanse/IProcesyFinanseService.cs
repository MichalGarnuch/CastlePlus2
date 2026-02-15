using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;

namespace CastlePlus2.Client.Services.Finanse
{
    public interface IProcesyFinanseService
    {
        Task<WystawFaktureContextDto> GetWystawFaktureContextAsync(CancellationToken ct = default);
        Task<WystawFaktureResultDto> WystawFaktureAsync(WystawFaktureRequest request, CancellationToken ct = default);
        Task<GenerateNajemFakturyResultDto> GenerateNajemFakturyAsync(GenerateNajemFakturyRequest request, CancellationToken ct = default);
        Task<PlatnoscContextDto> GetPlatnoscContextAsync(CancellationToken ct = default);
        Task<ZarejestrujPlatnoscResultDto> ZarejestrujPlatnoscAsync(ZarejestrujPlatnoscRequest request, CancellationToken ct = default);
    }
}