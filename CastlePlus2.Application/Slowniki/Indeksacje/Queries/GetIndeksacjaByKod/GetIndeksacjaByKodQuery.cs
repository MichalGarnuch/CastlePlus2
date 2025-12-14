using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.Indeksacje.Queries.GetIndeksacjaByKod
{
    public class GetIndeksacjaByKodQuery : IRequest<IndeksacjaDto?>
    {
        public string KodIndeksacji { get; set; } = default!;
    }
}
