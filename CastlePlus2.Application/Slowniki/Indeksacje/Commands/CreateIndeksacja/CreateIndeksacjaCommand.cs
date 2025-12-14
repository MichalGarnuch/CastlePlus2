using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.Indeksacje.Commands.CreateIndeksacja
{
    public class CreateIndeksacjaCommand : IRequest<IndeksacjaDto>
    {
        public string KodIndeksacji { get; set; } = default!;
        public string Nazwa { get; set; } = default!;
    }
}
