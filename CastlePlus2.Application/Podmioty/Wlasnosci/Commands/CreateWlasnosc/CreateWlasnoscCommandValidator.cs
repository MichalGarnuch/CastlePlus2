using FluentValidation;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Commands.CreateWlasnosc
{
    public sealed class CreateWlasnoscCommandValidator : AbstractValidator<CreateWlasnoscCommand>
    {
        public CreateWlasnoscCommandValidator()
        {
            // Reguła z SQL: IdEncji uniqueidentifier NOT NULL
            RuleFor(x => x.IdEncji)
                .NotEmpty();

            // Reguła z SQL: IdPodmiotu bigint NOT NULL
            RuleFor(x => x.IdPodmiotu)
                .NotEmpty();

            // Reguła z SQL: UdzialProcent decimal(7,4) NOT NULL
            RuleFor(x => x.UdzialProcent)
                .NotEmpty();

            // Reguła z SQL: OdDnia date NOT NULL
            RuleFor(x => x.OdDnia)
                .NotEmpty();
        }
    }
}