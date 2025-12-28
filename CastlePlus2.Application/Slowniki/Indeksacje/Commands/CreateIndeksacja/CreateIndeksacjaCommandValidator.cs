using FluentValidation;

namespace CastlePlus2.Application.Slowniki.Indeksacje.Commands.CreateIndeksacja
{
    public sealed class CreateIndeksacjaCommandValidator : AbstractValidator<CreateIndeksacjaCommand>
    {
        public CreateIndeksacjaCommandValidator()
        {
            // Rule from SQL: KodIndeksacji nvarchar(20) NOT NULL
            RuleFor(x => x.KodIndeksacji)
                .NotEmpty()
                .MaximumLength(20);

            // Rule from SQL: Nazwa nvarchar(100) NOT NULL
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(100);

            // Rule from SQL: AdresZrodlaURL nvarchar(400) NULL
            RuleFor(x => x.AdresZrodlaURL)
                .MaximumLength(400);
        }
    }
}