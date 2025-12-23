using MediatR;

namespace CastlePlus2.Application.Finanse.Platnosci.Commands.DeletePlatnosc
{
    public record DeletePlatnoscCommand(long IdPlatnosci) : IRequest<bool>;
}
