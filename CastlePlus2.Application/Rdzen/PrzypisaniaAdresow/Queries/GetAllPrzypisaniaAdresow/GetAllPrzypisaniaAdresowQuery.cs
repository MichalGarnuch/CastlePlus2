using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Queries.GetAllPrzypisaniaAdresow
{
    public sealed record GetAllPrzypisaniaAdresowQuery : IRequest<List<PrzypisanieAdresuDto>>;
}
