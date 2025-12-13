using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Queries.GetRozliczeniePlatnosciById
{
    public class GetRozliczeniePlatnosciByIdQuery : IRequest<RozliczeniePlatnosciDto?>
    {
        public long Id { get; }

        public GetRozliczeniePlatnosciByIdQuery(long id)
        {
            Id = id;
        }
    }
}
