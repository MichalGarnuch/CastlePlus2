using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Commands.RestoreUser
{
    public sealed record RestoreUserCommand(int UserId) : IRequest;
}