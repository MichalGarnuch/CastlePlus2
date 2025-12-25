using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Queries.GetAllRozliczeniaPlatnosci
{
    public record GetAllRozliczeniaPlatnosciQuery() : IRequest<List<RozliczeniePlatnosciDto>>;
}
