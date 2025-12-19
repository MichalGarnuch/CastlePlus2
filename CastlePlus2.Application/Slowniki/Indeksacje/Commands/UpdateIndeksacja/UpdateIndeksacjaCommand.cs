using MediatR;

namespace CastlePlus2.Application.Slowniki.Indeksacje.Commands.UpdateIndeksacja
{
    public class UpdateIndeksacjaCommand : IRequest<bool>
    {
        public string KodIndeksacji { get; set; } = default!;
        public string Nazwa { get; set; } = default!;
        public string? AdresZrodlaURL { get; set; }
    }
}
