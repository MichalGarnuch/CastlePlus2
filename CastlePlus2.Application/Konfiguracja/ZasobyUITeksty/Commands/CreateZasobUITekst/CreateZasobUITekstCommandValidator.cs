using FluentValidation;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Commands.CreateZasobUITekst
{
    public sealed class CreateZasobUITekstCommandValidator : AbstractValidator<CreateZasobUITekstCommand>
    {
        public CreateZasobUITekstCommandValidator()
        {
            RuleFor(x => x.IdEncji).NotEmpty();
            // Rule from SQL: Jezyk nvarchar(10) NOT NULL
            RuleFor(x => x.Jezyk).NotEmpty().MaximumLength(10);
            // Rule from SQL: Pole nvarchar(40) NOT NULL
            RuleFor(x => x.Pole).NotEmpty().MaximumLength(40);
            // Rule from SQL: Wartosc nvarchar(max) NOT NULL
            RuleFor(x => x.Wartosc).NotEmpty();
            // Rule from SQL: Format nvarchar(20) NOT NULL (DEFAULT 'Plain')
            RuleFor(x => x.Format).MaximumLength(20);
            // Rule from SQL: Sort int NOT NULL (DEFAULT 0)
            // technicznie bez ograniczeń zakresu w SQL → brak dodatkowych reguł
        }
    }
}