using CastlePlus2.Application.Interfaces.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Commands.SetUserRoles
{
    public sealed class SetUserRolesCommandHandler : IRequestHandler<SetUserRolesCommand, Unit>
    {
        private readonly IUzytkownikAuthRepository _repository;

        public SetUserRolesCommandHandler(IUzytkownikAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(SetUserRolesCommand request, CancellationToken ct)
        {
            await _repository.ReplaceUserRolesAsync(request.UserId, request.RoleCodes, ct);
            return Unit.Value;
        }
    }
}