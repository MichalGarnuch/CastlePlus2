using FluentValidation;

namespace CastlePlus2.Application.Rdzen.Lokale.Commands.CreateLokal
{
    public sealed class CreateLokalCommandValidator : AbstractValidator<CreateLokalCommand>
    {
        public CreateLokalCommandValidator()
        {
            // Reguła z SQL: IdBudynku uniqueidentifier NOT NULL
            RuleFor(x => x.IdBudynku)
                .NotEmpty();

            // Reguła z SQL: KodLokalu nvarchar(50) NOT NULL
            RuleFor(x => x.KodLokalu)
                .NotEmpty()
                .MaximumLength(50);

            // Reguła z SQL: Przeznaczenie nvarchar(60) NULL
            RuleFor(x => x.Przeznaczenie)
                .MaximumLength(60);
        }
    }
}