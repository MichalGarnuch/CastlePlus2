using CastlePlus2.Contracts.DTOs.Najem;

namespace CastlePlus2.Application.Interfaces.Najem
{
    public interface INajemAnalitykaQueryService
    {
        Task<IReadOnlyList<OblozenieLokaluDto>> GetOblozenieLokaliUtcDzisAsync(CancellationToken ct);
        Task<IReadOnlyList<RaportNajmuZaMiesiacRowDto>> GetRaportNajmuZaMiesiacAsync(int rok, int miesiac, CancellationToken ct);
    }
}