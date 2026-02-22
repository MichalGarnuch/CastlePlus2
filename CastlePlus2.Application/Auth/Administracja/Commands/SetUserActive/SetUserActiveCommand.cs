using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Commands.SetUserActive
{
    public sealed record SetUserActiveCommand(int UserId, bool IsActive) : IRequest;
}