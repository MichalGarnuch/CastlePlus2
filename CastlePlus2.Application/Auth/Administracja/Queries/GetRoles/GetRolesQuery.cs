using CastlePlus2.Contracts.DTOs.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Queries.GetRoles
{
    public sealed record GetRolesQuery : IRequest<RoleDto[]>;
}