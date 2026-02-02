using System.Security.Cryptography;
using System.Text;
using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Application.Interfaces.Notifications;
using CastlePlus2.Domain.Entities.Auth;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUzytkownikAuthRepository _userRepository;
        private readonly IActivationTokenRepository _activationTokenRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IEmailSender _emailSender;
        private readonly IAppUrlProvider _urlProvider;

        public CreateUserCommandHandler(
            IUzytkownikAuthRepository userRepository,
            IActivationTokenRepository activationTokenRepository,
            IPasswordHashService passwordHashService,
            IEmailSender emailSender,
            IAppUrlProvider urlProvider)
        {
            _userRepository = userRepository;
            _activationTokenRepository = activationTokenRepository;
            _passwordHashService = passwordHashService;
            _emailSender = emailSender;
            _urlProvider = urlProvider;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken ct)
        {
            if (await _userRepository.LoginExistsAsync(request.Login, ct))
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(CreateUserCommand.Login), "Login jest już zajęty.")
                });
            }

            if (await _userRepository.EmailExistsAsync(request.Email, ct))
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(CreateUserCommand.Email), "Email jest już zajęty.")
                });
            }

            foreach (var roleCode in request.RoleCodes)
            {
                if (!await _userRepository.RoleExistsByCodeAsync(roleCode, ct))
                {
                    throw new ValidationException(new[]
                    {
                        new ValidationFailure(nameof(CreateUserCommand.RoleCodes), $"Nie istnieje rola {roleCode}.")
                    });
                }
            }

            var utcNow = DateTime.UtcNow;
            var tempPassword = Guid.NewGuid().ToString("N");
            var user = new Uzytkownik
            {
                Login = request.Login,
                Email = request.Email,
                HasloHash = _passwordHashService.Hash(tempPassword),
                CzyAktywny = true,
                DataUtworzeniaUtc = utcNow,
                DataModyfikacjiUtc = utcNow
            };

            var userId = await _userRepository.CreateUserAsync(user, ct);
            foreach (var roleCode in request.RoleCodes)
            {
                var roleId = await _userRepository.GetRoleIdByCodeAsync(roleCode, ct);
                if (roleId.HasValue)
                {
                    await _userRepository.AssignRoleAsync(userId, roleId.Value, ct);
                }
            }

            var activationToken = GenerateToken();
            var activationHash = HashToken(activationToken);

            await _activationTokenRepository.AddAsync(new ActivationToken
            {
                IdUzytkownika = userId,
                TokenHash = activationHash,
                CreatedAtUtc = utcNow,
                ExpiresAtUtc = utcNow.AddHours(24)
            }, ct);

            var baseUrl = _urlProvider.GetClientBaseUrl().TrimEnd('/');
            var activationUrl = $"{baseUrl}/auth/activate?token={Uri.EscapeDataString(activationToken)}";
            var subject = "CastlePlus2: aktywuj konto";
            var body = $"""
Administrator utworzył dla Ciebie konto.

Login: {request.Login}
Ustaw hasło: {activationUrl}

Link jest ważny przez 24 godziny.
""";

            await _emailSender.SendAsync(new[] { request.Email }, subject, body, ct);
        }

        private static string GenerateToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(32);
            return Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .TrimEnd('=');
        }

        private static byte[] HashToken(string token)
        {
            using var sha = SHA256.Create();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(token));
        }
    }
}