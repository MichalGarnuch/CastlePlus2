using CastlePlus2.Contracts.DTOs.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Queries.GetUsersWithRoles
{
    public sealed record GetUsersWithRolesQuery : IRequest<AdminUserDto[]>;
}