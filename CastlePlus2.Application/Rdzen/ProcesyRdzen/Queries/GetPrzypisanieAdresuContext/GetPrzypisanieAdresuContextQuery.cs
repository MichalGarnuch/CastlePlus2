using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.ProcesyRdzen.Queries.GetPrzypisanieAdresuContext
{
    public sealed record GetPrzypisanieAdresuContextQuery : IRequest<PrzypisanieAdresuContextDto>;
}