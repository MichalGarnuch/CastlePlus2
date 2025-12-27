using FluentValidation;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.UpdateNieruchomosc
{
    public sealed class UpdateNieruchomoscCommandValidator : AbstractValidator<UpdateNieruchomoscCommand>
    {
        public UpdateNieruchomoscCommandValidator()
        {
            // Rule from SQL: IdEncji uniqueidentifier NOT NULL
            RuleFor(x => x.Id)
                .NotEmpty();

            // Rule from SQL: Nazwa nvarchar(200) NOT NULL
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
