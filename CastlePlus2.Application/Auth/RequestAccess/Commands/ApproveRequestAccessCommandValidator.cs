using FluentValidation;

namespace CastlePlus2.Application.Auth.RequestAccess.Commands
{
    public sealed class ApproveRequestAccessCommandValidator : AbstractValidator<ApproveRequestAccessCommand>
    {
        public ApproveRequestAccessCommandValidator()
        {
            RuleFor(x => x.RequestAccessId)
                .GreaterThan(0);
            // Rule from SQL: [auth].[RequestAccess].[IdRequestAccess] int NOT NULL IDENTITY(1,1)

            RuleFor(x => x.ApprovedBy)
                .NotEmpty()
                .MaximumLength(400);
            // Rule from SQL: [auth].[RequestAccess].[ApprovedBy] nvarchar(400) NULL (w praktyce wymagamy przy approve)

            RuleFor(x => x.Login)
                .NotEmpty()
                .MaximumLength(100);
            // Rule from SQL: [auth].[Uzytkownik].[Login] nvarchar(100) NOT NULL

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(200);
            // Rule from SQL: [auth].[Uzytkownik].[Email] nvarchar(200) NOT NULL

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(200);
            // Business rule: hasło wejściowe (hash trafia do [auth].[Uzytkownik].[HasloHash] nvarchar(400) NOT NULL)

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password)
                .WithMessage("Hasła muszą być identyczne.");
            // Business rule

            RuleFor(x => x.RoleCodes)
                .NotNull()
                .Must(codes => codes.Length > 0)
                .WithMessage("Wybierz co najmniej jedną rolę.");
        }
    }
}