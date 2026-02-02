using FluentValidation;

namespace CastlePlus2.Application.Auth.RequestAccess.Commands
{
    public sealed class RejectRequestAccessCommandValidator : AbstractValidator<RejectRequestAccessCommand>
    {
        public RejectRequestAccessCommandValidator()
        {
            RuleFor(x => x.RequestAccessId)
                .GreaterThan(0);

            RuleFor(x => x.RejectedBy)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Reason)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Reason));
        }
    }
}