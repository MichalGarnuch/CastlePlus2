using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Commands.DeleteBudynek
{
    public sealed record DeleteBudynekCommand(Guid Id) : IRequest<bool>;
}
