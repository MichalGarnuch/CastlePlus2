using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.Indeksacje.Queries.GetAllIndeksacje
{
    public class GetAllIndeksacjeQuery : IRequest<List<IndeksacjaDto>>
    {
    }
}
