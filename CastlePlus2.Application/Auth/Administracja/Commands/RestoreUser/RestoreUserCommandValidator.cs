using FluentValidation;

namespace CastlePlus2.Application.Auth.Administracja.Commands.RestoreUser
{
    public sealed class RestoreUserCommandValidator : AbstractValidator<RestoreUserCommand>
    {
        public RestoreUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);

            // Rule from SQL [auth].[Uzytkownik].[CzyUsuniety] bit NOT NULL.
            RuleFor(x => x)
                .NotNull();
        }
    }
}