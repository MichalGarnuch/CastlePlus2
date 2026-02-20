using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;

namespace CastlePlus2.Client.Services.Najem
{
    public interface INajemAnalitykaService
    {
        Task<IReadOnlyList<OblozenieLokaluDto>> GetOblozenieLokaliUtcDzisAsync(CancellationToken ct = default);
        Task<IReadOnlyList<RaportNajmuZaMiesiacRowDto>> GetRaportNajmuZaMiesiacAsync(GetRaportNajmuZaMiesiacRequest request, CancellationToken ct = default);
    }
}