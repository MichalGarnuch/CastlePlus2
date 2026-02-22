using FluentValidation;

namespace CastlePlus2.Application.Auth.Administracja.Commands.DeleteUser
{
    public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.DeletedByUserId)
                .GreaterThan(0);

            // Rule from SQL [auth].[Uzytkownik].[UsunietoPrzez] nvarchar(200) NULL.
            RuleFor(x => x.DeletedByLogin)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}