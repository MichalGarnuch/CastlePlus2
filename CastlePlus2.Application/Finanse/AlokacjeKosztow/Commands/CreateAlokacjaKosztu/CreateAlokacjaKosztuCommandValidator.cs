using FluentValidation;

namespace CastlePlus2.Application.Finanse.AlokacjeKosztow.Commands.CreateAlokacjaKosztu
{
    public sealed class CreateAlokacjaKosztuCommandValidator : AbstractValidator<CreateAlokacjaKosztuCommand>
    {
        public CreateAlokacjaKosztuCommandValidator()
        {
            // Reguła z SQL: IdPozycjiKosztu bigint NOT NULL
            RuleFor(x => x.IdPozycjiKosztu)
                .NotEmpty();

            // Reguła z SQL: IdEncji uniqueidentifier NOT NULL
            RuleFor(x => x.IdEncji)
                .NotEmpty();

            // Reguła z SQL: KwotaNetto decimal(18,2) NOT NULL
            RuleFor(x => x.KwotaNetto)
                .NotEmpty();

            // Reguła z SQL: KwotaBrutto decimal(18,2) NOT NULL
            RuleFor(x => x.KwotaBrutto)
                .NotEmpty();
        }
    }
}