using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Contracts.DTOs.Auth;
using CastlePlus2.Domain.Entities.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Refresh
{
    public sealed class RefreshCommandHandler : IRequestHandler<RefreshCommand, RefreshResult>
    {
        private readonly IUzytkownikAuthRepository _uzytkownikRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAuthTokenService _authTokenService;

        public RefreshCommandHandler(
            IUzytkownikAuthRepository uzytkownikRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IAuthTokenService authTokenService)
        {
            _uzytkownikRepository = uzytkownikRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _authTokenService = authTokenService;
        }

        public async Task<RefreshResult> Handle(RefreshCommand request, CancellationToken ct)
        {
            var utcNow = DateTime.UtcNow;

            var tokenHash = _authTokenService.HashRefreshToken(request.RefreshToken);
            var stored = await _refreshTokenRepository.FindByTokenHashAsync(tokenHash, ct);

            if (stored == null || stored.RevokedAtUtc.HasValue || stored.ExpiresAtUtc <= utcNow)
                throw new UnauthorizedAccessException("Nieprawidłowy refresh token.");

            var user = await _uzytkownikRepository.FindByIdAsync(stored.IdUzytkownika, ct);
            if (user == null || !user.CzyAktywny)
                throw new UnauthorizedAccessException("Nieprawidłowy refresh token.");

            var roles = await _uzytkownikRepository.GetRoleCodesAsync(user.IdUzytkownika, ct);
            var accessToken = _authTokenService.CreateAccessToken(user.IdUzytkownika, user.Login, roles, utcNow);

            var newRefreshToken = _authTokenService.CreateRefreshToken();
            var newRefreshHash = _authTokenService.HashRefreshToken(newRefreshToken);

            await _refreshTokenRepository.RevokeAsync(stored.IdRefreshToken, utcNow, ct);

            var refreshEntity = new RefreshToken
            {
                IdUzytkownika = user.IdUzytkownika,
                TokenHash = newRefreshHash,
                CreatedAtUtc = utcNow,
                ExpiresAtUtc = _authTokenService.GetRefreshTokenExpiresAtUtc(utcNow),
                DeviceInfo = request.DeviceInfo
            };

            await _refreshTokenRepository.AddAsync(refreshEntity, ct);

            return new RefreshResult
            {
                Tokens = new AuthTokensDto
                {
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken,
                    AccessTokenExpiresAtUtc = _authTokenService.GetAccessTokenExpiresAtUtc(utcNow)
                }
            };
        }
    }
}
