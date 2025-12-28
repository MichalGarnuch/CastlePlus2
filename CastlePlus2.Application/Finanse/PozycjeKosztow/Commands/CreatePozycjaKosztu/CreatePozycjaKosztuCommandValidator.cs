using FluentValidation;

namespace CastlePlus2.Application.Finanse.PozycjeKosztow.Commands.CreatePozycjaKosztu
{
    public sealed class CreatePozycjaKosztuCommandValidator : AbstractValidator<CreatePozycjaKosztuCommand>
    {
        public CreatePozycjaKosztuCommandValidator()
        {
            // Rule from SQL: IdFaktury bigint NOT NULL
            RuleFor(x => x.IdFaktury)
                .NotEmpty();

            // Rule from SQL: IdKategoriiKosztu bigint NOT NULL
            RuleFor(x => x.IdKategoriiKosztu)
                .NotEmpty();

            // Rule from SQL: Opis nvarchar(200) NULL
            RuleFor(x => x.Opis)
                .MaximumLength(200);

            // Rule from SQL: KwotaNetto decimal(18,2) NOT NULL
            RuleFor(x => x.KwotaNetto)
                .NotEmpty();

            // Rule from SQL: KwotaBrutto decimal(18,2) NOT NULL
            RuleFor(x => x.KwotaBrutto)
                .NotEmpty();
        }
    }
}