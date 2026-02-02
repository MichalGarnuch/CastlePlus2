using FluentValidation;

namespace CastlePlus2.Application.Auth.RequestAccess.Commands
{
    public sealed class CreateRequestAccessCommandValidator : AbstractValidator<CreateRequestAccessCommand>
    {
        public CreateRequestAccessCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(200);

            RuleFor(x => x.Login)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.Login));

            RuleFor(x => x.Phone)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.Phone));

            RuleFor(x => x.Department)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Justification)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}