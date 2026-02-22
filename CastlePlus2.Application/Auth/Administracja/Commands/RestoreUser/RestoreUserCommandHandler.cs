using CastlePlus2.Application.Interfaces.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Commands.RestoreUser
{
    public sealed class RestoreUserCommandHandler : IRequestHandler<RestoreUserCommand>
    {
        private readonly IUzytkownikAuthRepository _repository;

        public RestoreUserCommandHandler(IUzytkownikAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RestoreUserCommand request, CancellationToken ct)
        {
            var restored = await _repository.RestoreUserAsync(request.UserId, DateTime.UtcNow, ct);
            if (!restored)
            {
                throw new KeyNotFoundException("Użytkownik nie istnieje.");
            }
        }
    }
}