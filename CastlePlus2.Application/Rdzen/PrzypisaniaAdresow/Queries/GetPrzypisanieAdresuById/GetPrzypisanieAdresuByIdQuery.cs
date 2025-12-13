using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Queries.GetPrzypisanieAdresuById
{
    public sealed record GetPrzypisanieAdresuByIdQuery(long Id)
        : IRequest<PrzypisanieAdresuDto?>;
}
