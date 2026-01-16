using CastlePlus2.Application.Interfaces.Auth;
using FluentValidation;

namespace CastlePlus2.Application.Auth.Administracja.Commands.SetUserRoles
{
    public sealed class SetUserRolesCommandValidator : AbstractValidator<SetUserRolesCommand>
    {
        public SetUserRolesCommandValidator(IUzytkownikAuthRepository repository)
        {
            RuleFor(x => x.RoleCodes)
                .NotNull()
                .NotEmpty()
                .WithMessage("Lista ról nie może być pusta.");

            RuleForEach(x => x.RoleCodes)
                .NotEmpty()
                .WithMessage("Kod roli nie może być pusty.")
                .MustAsync(async (roleCode, ct) => await repository.RoleExistsByCodeAsync(roleCode, ct))
                .WithMessage("Wybrana rola nie istnieje.");
        }
    }
}