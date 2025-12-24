using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.PozycjeKosztow.Queries.GetAllPozycjeKosztow
{
    public record GetAllPozycjeKosztowQuery() : IRequest<List<PozycjaKosztuDto>>;
}
