using FluentValidation;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUI.Commands.CreateZasobUI
{
    public sealed class CreateZasobUICommandValidator : AbstractValidator<CreateZasobUICommand>
    {
        public CreateZasobUICommandValidator()
        {
            // Rule from SQL: KodZasobu nvarchar(120) NOT NULL. (Reguła z SQL)
            RuleFor(x => x.KodZasobu)
                .NotEmpty()
                .MaximumLength(120);

            // Rule from SQL: Typ nvarchar(30) NOT NULL. (Reguła z SQL)
            RuleFor(x => x.Typ)
                .NotEmpty()
                .MaximumLength(30);

            // Rule from SQL: Kategoria nvarchar(60) NULL. (Reguła z SQL)
            RuleFor(x => x.Kategoria)
                .MaximumLength(60)
                .When(x => !string.IsNullOrWhiteSpace(x.Kategoria));

            // Rule from SQL: CzyAktywny bit NOT NULL. (Reguła z SQL)
            RuleFor(x => x.CzyAktywny)
                .NotNull();

            // Rule from SQL: Sort int NOT NULL. (Reguła z SQL)
            RuleFor(x => x.Sort)
                .NotNull();

            RuleFor(x => x.WazneDoUtc)
                .GreaterThanOrEqualTo(x => x.WazneOdUtc)
                .When(x => x.WazneOdUtc.HasValue && x.WazneDoUtc.HasValue)
                .WithMessage("Data 'Ważne do' nie może być wcześniejsza niż 'Ważne od'.");
        }
    }
}