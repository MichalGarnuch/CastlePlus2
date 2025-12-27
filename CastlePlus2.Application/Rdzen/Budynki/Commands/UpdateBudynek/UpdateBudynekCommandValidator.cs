using FluentValidation;

namespace CastlePlus2.Application.Rdzen.Budynki.Commands.UpdateBudynek
{
    public sealed class UpdateBudynekCommandValidator : AbstractValidator<UpdateBudynekCommand>
    {
        public UpdateBudynekCommandValidator()
        {
            // Rule from SQL: IdEncji uniqueidentifier NOT NULL
            RuleFor(x => x.Id)
                .NotEmpty();

            // Rule from SQL: IdNieruchomosci uniqueidentifier NOT NULL
            RuleFor(x => x.IdNieruchomosci)
                .NotEmpty();

            // Rule from SQL: KodBudynku nvarchar(50) NOT NULL
            RuleFor(x => x.KodBudynku)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
