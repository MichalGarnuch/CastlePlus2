using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Commands.UpdatePrzypisanieAdresu
{
    public sealed record UpdatePrzypisanieAdresuCommand(
        long IdPrzypisaniaAdresu,
        UpdatePrzypisanieAdresuRequest Request
    ) : IRequest<PrzypisanieAdresuDto?>;
}
