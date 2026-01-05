using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.ProcesyPlatnosci.Queries.GetPlatnoscContext
{
    public record GetPlatnoscContextQuery() : IRequest<PlatnoscContextDto>;
}