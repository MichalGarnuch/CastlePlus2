using FluentValidation;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.ChangePassword
{
    public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.CurrentPassword)
                .NotEmpty();

            // Rule from SQL: auth.Uzytkownik.HasloHash nvarchar(200) NOT NULL. (Reguła z SQL)
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(200);

            RuleFor(x => x.ConfirmNewPassword)
                .Equal(x => x.NewPassword)
                .WithMessage("Potwierdzenie nowego hasła musi być zgodne z nowym hasłem.");
        }
    }
}