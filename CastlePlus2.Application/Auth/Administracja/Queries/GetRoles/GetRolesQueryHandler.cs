using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Contracts.DTOs.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Queries.GetRoles
{
    public sealed class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, RoleDto[]>
    {
        private readonly IUzytkownikAuthRepository _repository;

        public GetRolesQueryHandler(IUzytkownikAuthRepository repository)
        {
            _repository = repository;
        }

        public Task<RoleDto[]> Handle(GetRolesQuery request, CancellationToken ct)
        {
            return _repository.GetRolesAsync(ct);
        }
    }
}