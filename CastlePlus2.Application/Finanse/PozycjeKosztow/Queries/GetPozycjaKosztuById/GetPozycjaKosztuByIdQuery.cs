using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.PozycjeKosztow.Queries.GetPozycjaKosztuById
{
    public class GetPozycjaKosztuByIdQuery : IRequest<PozycjaKosztuDto?>
    {
        public long Id { get; }

        public GetPozycjaKosztuByIdQuery(long id)
        {
            Id = id;
        }
    }
}
