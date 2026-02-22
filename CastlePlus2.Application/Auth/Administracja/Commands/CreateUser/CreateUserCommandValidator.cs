using FluentValidation;

namespace CastlePlus2.Application.Auth.Administracja.Commands.CreateUser
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Login)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(200);

            // Rule from SQL: auth.Uzytkownik.HasloHash nvarchar(200) NOT NULL. (Reguła z SQL)
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(200);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Potwierdzenie hasła musi być zgodne z hasłem.");

            RuleFor(x => x.RoleCodes)
                .NotNull()
                .Must(codes => codes.Length > 0)
                .WithMessage("Wybierz co najmniej jedną rolę.");
        }
    }
}