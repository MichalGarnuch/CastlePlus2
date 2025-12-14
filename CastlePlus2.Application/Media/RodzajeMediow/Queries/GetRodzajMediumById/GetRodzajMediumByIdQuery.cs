using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.RodzajeMediow.Queries.GetRodzajMediumById
{
    public class GetRodzajMediumByIdQuery : IRequest<RodzajMediumDto?>
    {
        public string KodRodzaju { get; }

        public GetRodzajMediumByIdQuery(string kodRodzaju)
        {
            KodRodzaju = kodRodzaju;
        }
    }
}
