using FluentValidation;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.CreatePowiazanieZlecenia
{
    public sealed class CreatePowiazanieZleceniaCommandValidator : AbstractValidator<CreatePowiazanieZleceniaCommand>
    {
        public CreatePowiazanieZleceniaCommandValidator()
        {
            // Reguła z SQL: IdZlecenia bigint NOT NULL
            RuleFor(x => x.IdZlecenia)
                .NotEmpty();

            // Reguła z SQL: IdEncji uniqueidentifier NOT NULL
            RuleFor(x => x.IdEncji)
                .NotEmpty();
        }
    }
}