using FluentValidation;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Commands.CreateZasobUITekst
{
    public sealed class CreateZasobUITekstCommandValidator : AbstractValidator<CreateZasobUITekstCommand>
    {
        public CreateZasobUITekstCommandValidator()
        {
            // Rule from SQL: IdEncji uniqueidentifier NOT NULL. (Reguła z SQL)
            RuleFor(x => x.IdEncji)
                .NotEmpty();

            // Rule from SQL: Jezyk nvarchar(10) NOT NULL. (Reguła z SQL)
            RuleFor(x => x.Jezyk)
                .NotEmpty()
                .MaximumLength(10);

            // Rule from SQL: Pole nvarchar(40) NOT NULL. (Reguła z SQL)
            RuleFor(x => x.Pole)
                .NotEmpty()
                .MaximumLength(40);

            // Rule from SQL: Wartosc nvarchar(max) NOT NULL. (Reguła z SQL)
            RuleFor(x => x.Wartosc)
                .NotEmpty();

            // Rule from SQL: Format nvarchar(20) NOT NULL. (Reguła z SQL)
            RuleFor(x => x.Format)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}