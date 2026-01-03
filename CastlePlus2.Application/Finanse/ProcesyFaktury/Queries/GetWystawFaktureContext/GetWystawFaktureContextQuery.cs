using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Queries.GetWystawFaktureContext
{
    public record GetWystawFaktureContextQuery() : IRequest<WystawFaktureContextDto>;
}