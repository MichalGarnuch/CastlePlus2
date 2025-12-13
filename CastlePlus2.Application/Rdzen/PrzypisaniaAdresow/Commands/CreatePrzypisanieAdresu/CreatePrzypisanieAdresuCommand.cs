using System;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Commands.CreatePrzypisanieAdresu
{
    public sealed record CreatePrzypisanieAdresuCommand(
        Guid IdEncji,
        long IdAdresu,
        DateOnly OdDnia
    ) : IRequest<PrzypisanieAdresuDto>;
}
