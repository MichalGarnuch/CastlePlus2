using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;

namespace CastlePlus2.Client.Services.Najem
{
    public interface IProcesyNajmuService
    {
        Task<AneksujCzynszResult> AneksujCzynszAsync(Guid idUmowyNajmu, AneksujCzynszRequest request, CancellationToken ct = default);
        Task<ZakonczUmoweNajmuResult> ZakonczUmoweAsync(Guid idUmowyNajmu, ZakonczUmoweNajmuRequest request, CancellationToken ct = default);
    }
}