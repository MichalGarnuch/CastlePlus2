using MediatR;

namespace CastlePlus2.Application.Finanse.AlokacjeKosztow.Commands.DeleteAlokacjaKosztu
{
    public record DeleteAlokacjaKosztuCommand(long IdAlokacji) : IRequest<bool>;
}
