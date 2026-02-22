using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Contracts.DTOs.Auth;
using CastlePlus2.Domain.Entities.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Login
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly IUzytkownikAuthRepository _uzytkownikRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAuthTokenService _authTokenService;
        private readonly IPasswordHashService _passwordHashService;

        public LoginCommandHandler(
            IUzytkownikAuthRepository uzytkownikRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IAuthTokenService authTokenService,
            IPasswordHashService passwordHashService)
        {
            _uzytkownikRepository = uzytkownikRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _authTokenService = authTokenService;
            _passwordHashService = passwordHashService;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken ct)
        {
            var loginOrEmail = request.LoginOrEmail?.Trim() ?? string.Empty;
            var user = await _uzytkownikRepository.FindByLoginOrEmailAsync(loginOrEmail, ct);
            if (user == null)
                throw new UnauthorizedAccessException("Nieprawidłowy login/email lub hasło.");

            if (user.CzyUsuniety)
                throw new UnauthorizedAccessException("Konto zostało usunięte.");

            if (!user.CzyAktywny)
                throw new UnauthorizedAccessException("Konto jest zablokowane.");

            if (!_passwordHashService.Verify(request.Password, user.HasloHash))
                throw new UnauthorizedAccessException("Nieprawidłowy login/email lub hasło.");

            var utcNow = DateTime.UtcNow;

            var roles = await _uzytkownikRepository.GetRoleCodesAsync(user.IdUzytkownika, ct);
            var accessToken = _authTokenService.CreateAccessToken(user.IdUzytkownika, user.Login, roles, utcNow);

            var refreshToken = _authTokenService.CreateRefreshToken();
            var refreshHash = _authTokenService.HashRefreshToken(refreshToken);

            var refreshEntity = new RefreshToken
            {
                IdUzytkownika = user.IdUzytkownika,
                TokenHash = refreshHash,
                CreatedAtUtc = utcNow,
                ExpiresAtUtc = _authTokenService.GetRefreshTokenExpiresAtUtc(utcNow),
                DeviceInfo = request.DeviceInfo
            };

            await _refreshTokenRepository.AddAsync(refreshEntity, ct);
            await _uzytkownikRepository.UpdateLastLoginAsync(user.IdUzytkownika, utcNow, ct);

            return new LoginResult
            {
                Tokens = new AuthTokensDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    AccessTokenExpiresAtUtc = _authTokenService.GetAccessTokenExpiresAtUtc(utcNow)
                }
            };
        }
    }
}
