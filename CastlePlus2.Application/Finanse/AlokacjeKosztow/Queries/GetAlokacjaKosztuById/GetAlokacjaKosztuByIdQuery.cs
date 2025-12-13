using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.AlokacjeKosztow.Queries.GetAlokacjaKosztuById
{
    public class GetAlokacjaKosztuByIdQuery : IRequest<AlokacjaKosztuDto?>
    {
        public long IdAlokacji { get; }

        public GetAlokacjaKosztuByIdQuery(long idAlokacji)
        {
            IdAlokacji = idAlokacji;
        }
    }
}
