using FluentValidation;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Refresh
{
    public sealed class RefreshCommandValidator : AbstractValidator<RefreshCommand>
    {
        public RefreshCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.DeviceInfo)
                .MaximumLength(200);
        }
    }
}
