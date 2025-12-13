using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Platnosci.Queries.GetPlatnoscById
{
    public class GetPlatnoscByIdQuery : IRequest<PlatnoscDto?>
    {
        public long Id { get; }

        public GetPlatnoscByIdQuery(long id)
        {
            Id = id;
        }
    }
}
