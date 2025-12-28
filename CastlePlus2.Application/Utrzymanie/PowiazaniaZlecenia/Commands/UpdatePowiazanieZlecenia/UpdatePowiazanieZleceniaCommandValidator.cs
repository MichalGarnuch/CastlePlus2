using FluentValidation;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.UpdatePowiazanieZlecenia
{
    public sealed class UpdatePowiazanieZleceniaCommandValidator : AbstractValidator<UpdatePowiazanieZleceniaCommand>
    {
        public UpdatePowiazanieZleceniaCommandValidator()
        {
            // Reguła z SQL: IdPowiazania bigint NOT NULL
            RuleFor(x => x.IdPowiazania)
                .NotEmpty();

            // Reguła z SQL: IdZlecenia bigint NOT NULL
            RuleFor(x => x.IdZlecenia)
                .NotEmpty();

            // Reguła z SQL: IdEncji uniqueidentifier NOT NULL
            RuleFor(x => x.IdEncji)
                .NotEmpty();
        }
    }
}