using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Commands.DeleteUser
{
    public sealed record DeleteUserCommand(int UserId, int DeletedByUserId, string DeletedByLogin) : IRequest;
}