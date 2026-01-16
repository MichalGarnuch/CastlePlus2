using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Contracts.DTOs.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Queries.GetUsersWithRoles
{
    public sealed class GetUsersWithRolesQueryHandler : IRequestHandler<GetUsersWithRolesQuery, AdminUserDto[]>
    {
        private readonly IUzytkownikAuthRepository _repository;

        public GetUsersWithRolesQueryHandler(IUzytkownikAuthRepository repository)
        {
            _repository = repository;
        }

        public Task<AdminUserDto[]> Handle(GetUsersWithRolesQuery request, CancellationToken ct)
        {
            return _repository.GetUsersWithRolesAsync(ct);
        }
    }
}