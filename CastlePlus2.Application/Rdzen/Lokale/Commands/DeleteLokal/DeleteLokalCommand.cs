using MediatR;

namespace CastlePlus2.Application.Rdzen.Lokale.Commands.DeleteLokal
{
    public sealed record DeleteLokalCommand(Guid Id) : IRequest<bool>;
}
