using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Queries.GetAllKategorieKosztow
{
    public record GetAllKategorieKosztowQuery() : IRequest<List<KategoriaKosztuDto>>;
}
