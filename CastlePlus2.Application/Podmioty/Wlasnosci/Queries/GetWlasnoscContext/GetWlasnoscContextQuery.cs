using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Queries.GetWlasnoscContext
{
    public record GetWlasnoscContextQuery() : IRequest<WlasnoscContextDto>;
}
