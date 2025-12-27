using FluentValidation;

namespace CastlePlus2.Application.Rdzen.Budynki.Commands.CreateBudynek
{
    public sealed class CreateBudynekCommandValidator : AbstractValidator<CreateBudynekCommand>
    {
        public CreateBudynekCommandValidator()
        {
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
