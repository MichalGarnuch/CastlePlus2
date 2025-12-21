using MediatR;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.DeleteNieruchomosc
{
    public sealed record DeleteNieruchomoscCommand(Guid Id) : IRequest<bool>;
}
