using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Contracts.DTOs.Auth;
using CastlePlus2.Domain.Entities.Auth;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Register
{
    public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResult>
    {
        private const string AdminRoleCode = "Admin";
        private const string AdminRoleCodeFallback = "ADMIN";
        private const string UserRoleCode = "User";
        private const string UserRoleCodeFallback = "USER";

        private readonly IUzytkownikAuthRepository _uzytkownikRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAuthTokenService _authTokenService;
        private readonly IPasswordHashService _passwordHashService;

        public RegisterCommandHandler(
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

        public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken ct)
        {
            if (await _uzytkownikRepository.LoginExistsAsync(request.Login, ct))
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(RegisterCommand.Login), "Login jest już zajęty.")
                });
            }

            if (!string.IsNullOrWhiteSpace(request.Email)
                && await _uzytkownikRepository.EmailExistsAsync(request.Email, ct))
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(RegisterCommand.Email), "Email jest już zajęty.")
                });
            }

            var hasUsers = await _uzytkownikRepository.AnyUsersAsync(ct);

            // Pierwszy user -> ADMIN, kolejne -> USER (z fallbackiem jeśli brak USER w DB)
            var (roleCode, roleId) = await GetRoleIdWithFallbackAsync(
                hasUsers ? UserRoleCode : AdminRoleCode,
                hasUsers ? UserRoleCodeFallback : AdminRoleCodeFallback,
                ct);

            if (roleId == null && hasUsers)
            {
                (roleCode, roleId) = await GetRoleIdWithFallbackAsync(AdminRoleCode, AdminRoleCodeFallback, ct);
            }

            if (roleId == null)
            {
                throw new InvalidOperationException($"Brak roli {roleCode} w systemie.");
            }

            var utcNow = DateTime.UtcNow;

            var user = new Uzytkownik
            {
                Login = request.Login,
                Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email,
                HasloHash = _passwordHashService.Hash(request.Password),
                CzyAktywny = true,
                DataUtworzeniaUtc = utcNow,
                DataModyfikacjiUtc = utcNow
            };

            var userId = await _uzytkownikRepository.CreateUserAsync(user, ct);
            await _uzytkownikRepository.AssignRoleAsync(userId, roleId.Value, ct);

            var roles = await _uzytkownikRepository.GetRoleCodesAsync(userId, ct);
            var accessToken = _authTokenService.CreateAccessToken(userId, user.Login, roles, utcNow);

            var refreshToken = _authTokenService.CreateRefreshToken();
            var refreshHash = _authTokenService.HashRefreshToken(refreshToken);

            var refreshEntity = new RefreshToken
            {
                IdUzytkownika = userId,
                TokenHash = refreshHash,
                CreatedAtUtc = utcNow,
                ExpiresAtUtc = _authTokenService.GetRefreshTokenExpiresAtUtc(utcNow),
                DeviceInfo = request.DeviceInfo
            };

            await _refreshTokenRepository.AddAsync(refreshEntity, ct);

            return new RegisterResult
            {
                Tokens = new AuthTokensDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    AccessTokenExpiresAtUtc = _authTokenService.GetAccessTokenExpiresAtUtc(utcNow)
                }
            };
        }

        private async Task<(string RoleCode, int? RoleId)> GetRoleIdWithFallbackAsync(
            string primaryRoleCode,
            string fallbackRoleCode,
            CancellationToken ct)
        {
            var roleId = await _uzytkownikRepository.GetRoleIdByCodeAsync(primaryRoleCode, ct);
            if (roleId.HasValue)
            {
                return (primaryRoleCode, roleId);
            }

            roleId = await _uzytkownikRepository.GetRoleIdByCodeAsync(fallbackRoleCode, ct);
            return (fallbackRoleCode, roleId);
        }
    }
}
