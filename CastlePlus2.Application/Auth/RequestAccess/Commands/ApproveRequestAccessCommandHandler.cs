using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Domain.Entities.Auth;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CastlePlus2.Application.Auth.RequestAccess.Commands
{
    public sealed class ApproveRequestAccessCommandHandler : IRequestHandler<ApproveRequestAccessCommand>
    {
        private readonly IAccessRequestRepository _requestRepository;
        private readonly IUzytkownikAuthRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;

        public ApproveRequestAccessCommandHandler(
            IAccessRequestRepository requestRepository,
            IUzytkownikAuthRepository userRepository,
            IPasswordHashService passwordHashService)
        {
            _requestRepository = requestRepository;
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
        }

        public async Task Handle(ApproveRequestAccessCommand request, CancellationToken ct)
        {
            var entry = await _requestRepository.GetByIdAsync(request.RequestAccessId, ct);
            if (entry is null)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(ApproveRequestAccessCommand.RequestAccessId), "Nie znaleziono zgłoszenia.")
                });
            }

            if (entry.Status != RequestAccessStatus.Pending)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(ApproveRequestAccessCommand.RequestAccessId), "Zgłoszenie nie jest w statusie Pending.")
                });
            }

            if (await _userRepository.LoginExistsAsync(request.Login, ct))
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(ApproveRequestAccessCommand.Login), "Login jest już zajęty.")
                });
            }

            if (await _userRepository.EmailExistsAsync(request.Email, ct))
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(ApproveRequestAccessCommand.Email), "Email jest już zajęty.")
                });
            }

            foreach (var roleCode in request.RoleCodes)
            {
                if (!await _userRepository.RoleExistsByCodeAsync(roleCode, ct))
                {
                    throw new ValidationException(new[]
                    {
                        new ValidationFailure(nameof(ApproveRequestAccessCommand.RoleCodes), $"Nie istnieje rola {roleCode}.")
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

            entry.Status = RequestAccessStatus.Approved;
            entry.UpdatedAtUtc = utcNow;
            entry.ApprovedBy = request.ApprovedBy;
            entry.ApprovedAtUtc = utcNow;
            entry.ApprovedLogin = request.Login;
            entry.ApprovedEmail = request.Email;
            entry.ApprovedRoleCodes = string.Join(",", request.RoleCodes);

            await _requestRepository.UpdateAsync(entry, ct);

            // Koniec: brak losowych haseł, brak aktywacji mailowej, brak tokenów aktywacji.
        }
    }
}