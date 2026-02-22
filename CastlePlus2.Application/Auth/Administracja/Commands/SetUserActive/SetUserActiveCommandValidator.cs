using FluentValidation;

namespace CastlePlus2.Application.Auth.Administracja.Commands.SetUserActive
{
    public sealed class SetUserActiveCommandValidator : AbstractValidator<SetUserActiveCommand>
    {
        public SetUserActiveCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);

            // Rule from SQL [auth].[Uzytkownik].[CzyAktywny] bit NOT NULL.
            RuleFor(x => x.IsActive)
                .NotNull();
        }
    }
}