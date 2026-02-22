using CastlePlus2.Application.Interfaces.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.ChangePassword
{
    public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUzytkownikAuthRepository _uzytkownikRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public ChangePasswordCommandHandler(
            IUzytkownikAuthRepository uzytkownikRepository,
            IPasswordHashService passwordHashService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _uzytkownikRepository = uzytkownikRepository;
            _passwordHashService = passwordHashService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task Handle(ChangePasswordCommand request, CancellationToken ct)
        {
            var user = await _uzytkownikRepository.FindByIdAsync(request.UserId, ct)
                ?? throw new KeyNotFoundException("Użytkownik nie istnieje.");

            if (!user.CzyAktywny)
            {
                throw new UnauthorizedAccessException("Konto jest zablokowane.");
            }

            if (!_passwordHashService.Verify(request.CurrentPassword, user.HasloHash))
            {
                throw new UnauthorizedAccessException("Aktualne hasło jest nieprawidłowe.");
            }

            var utcNow = DateTime.UtcNow;
            var newPasswordHash = _passwordHashService.Hash(request.NewPassword);

            await _uzytkownikRepository.UpdatePasswordAsync(request.UserId, newPasswordHash, utcNow, ct);
            await _refreshTokenRepository.RevokeAllForUserAsync(request.UserId, utcNow, ct);
        }
    }
}