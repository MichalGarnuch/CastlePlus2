using System.Security.Cryptography;
using System.Text;
using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Domain.Entities.Auth;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CastlePlus2.Application.Auth.RequestAccess.Commands
{
    public sealed class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand>
    {
        private readonly IActivationTokenRepository _activationTokenRepository;
        private readonly IUzytkownikAuthRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;

        public ActivateAccountCommandHandler(
            IActivationTokenRepository activationTokenRepository,
            IUzytkownikAuthRepository userRepository,
            IPasswordHashService passwordHashService)
        {
            _activationTokenRepository = activationTokenRepository;
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
        }

        public async Task Handle(ActivateAccountCommand request, CancellationToken ct)
        {
            var tokenHash = HashToken(request.Token);
            var activation = await _activationTokenRepository.FindByHashAsync(tokenHash, ct);

            if (activation is null)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(ActivateAccountCommand.Token), "Token aktywacyjny jest nieprawidłowy.")
                });
            }

            if (activation.ExpiresAtUtc < DateTime.UtcNow)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(ActivateAccountCommand.Token), "Token aktywacyjny wygasł.")
                });
            }

            var passwordHash = _passwordHashService.Hash(request.Password);
            var utcNow = DateTime.UtcNow;
            await _userRepository.UpdatePasswordAsync(activation.IdUzytkownika, passwordHash, utcNow, ct);
            await _activationTokenRepository.MarkUsedAsync(activation, utcNow, ct);
        }

        private static byte[] HashToken(string token)
        {
            using var sha = SHA256.Create();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(token));
        }
    }
}