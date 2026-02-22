using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Domain.Entities.Auth;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUzytkownikAuthRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;

        public CreateUserCommandHandler(
            IUzytkownikAuthRepository userRepository,
            IPasswordHashService passwordHashService)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
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
            var user = new Uzytkownik
            {
                Login = request.Login,
                Email = request.Email,
                HasloHash = _passwordHashService.Hash(request.Password),
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
        }
    }
}