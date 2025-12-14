using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Queries.GetOdczytById
{
    public class GetOdczytByIdQuery : IRequest<OdczytDto?>
    {
        public long IdOdczytu { get; }

        public GetOdczytByIdQuery(long idOdczytu)
        {
            IdOdczytu = idOdczytu;
        }
    }
}
