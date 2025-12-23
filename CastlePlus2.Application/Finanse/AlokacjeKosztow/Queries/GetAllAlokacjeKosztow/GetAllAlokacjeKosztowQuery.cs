using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.AlokacjeKosztow.Queries.GetAllAlokacjeKosztow
{
    public record GetAllAlokacjeKosztowQuery() : IRequest<List<AlokacjaKosztuDto>>;
}
