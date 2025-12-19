using MediatR;

namespace CastlePlus2.Application.Slowniki.Indeksacje.Commands.DeleteIndeksacja
{
    public class DeleteIndeksacjaCommand : IRequest<bool>
    {
        public string KodIndeksacji { get; set; } = default!;
    }
}
