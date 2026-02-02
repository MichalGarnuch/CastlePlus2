using FluentValidation;

namespace CastlePlus2.Application.Auth.RequestAccess.Commands
{
    public sealed class ApproveRequestAccessCommandValidator : AbstractValidator<ApproveRequestAccessCommand>
    {
        public ApproveRequestAccessCommandValidator()
        {
            RuleFor(x => x.RequestAccessId)
                .GreaterThan(0);

            RuleFor(x => x.ApprovedBy)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Login)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(200);

            RuleFor(x => x.RoleCodes)
                .NotNull()
                .Must(codes => codes.Length > 0)
                .WithMessage("Wybierz co najmniej jedną rolę.");
        }
    }
}