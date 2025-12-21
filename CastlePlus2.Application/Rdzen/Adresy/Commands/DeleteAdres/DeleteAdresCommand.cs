using MediatR;

namespace CastlePlus2.Application.Rdzen.Adresy.Commands.DeleteAdres
{
    public record DeleteAdresCommand(long IdAdresu) : IRequest<bool>;
}
