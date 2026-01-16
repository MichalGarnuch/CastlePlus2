using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Commands.SetUserRoles
{
    public sealed record SetUserRolesCommand(int UserId, string[] RoleCodes) : IRequest<Unit>;
}