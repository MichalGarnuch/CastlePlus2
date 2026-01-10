using CastlePlus2.Contracts.DTOs.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Queries.GetMe
{
    public sealed record GetMeQuery(int UserId) : IRequest<CurrentUserDto?>;
}
