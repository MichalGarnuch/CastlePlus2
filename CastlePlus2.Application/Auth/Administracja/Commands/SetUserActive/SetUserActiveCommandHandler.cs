using CastlePlus2.Application.Interfaces.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Commands.SetUserActive
{
    public sealed class SetUserActiveCommandHandler : IRequestHandler<SetUserActiveCommand>
    {
        private readonly IUzytkownikAuthRepository _repository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public SetUserActiveCommandHandler(IUzytkownikAuthRepository repository, IRefreshTokenRepository refreshTokenRepository)
        {
            _repository = repository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task Handle(SetUserActiveCommand request, CancellationToken ct)
        {
            var utcNow = DateTime.UtcNow;
            var updated = await _repository.SetUserActiveAsync(request.UserId, request.IsActive, utcNow, ct);
            if (!updated)
            {
                throw new KeyNotFoundException("Użytkownik nie istnieje.");
            }

            if (!request.IsActive)
            {
                await _refreshTokenRepository.RevokeAllForUserAsync(request.UserId, utcNow, ct);
            }
        }
    }
}