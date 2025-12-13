using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Queries.GetKategoriaKosztuById
{
    public class GetKategoriaKosztuByIdQuery : IRequest<KategoriaKosztuDto?>
    {
        public long Id { get; }

        public GetKategoriaKosztuByIdQuery(long id)
        {
            Id = id;
        }
    }
}
