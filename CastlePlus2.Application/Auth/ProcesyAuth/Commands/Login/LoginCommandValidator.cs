using FluentValidation;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Login
{
    public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.LoginOrEmail)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.DeviceInfo)
                .MaximumLength(200);
        }
    }
}
