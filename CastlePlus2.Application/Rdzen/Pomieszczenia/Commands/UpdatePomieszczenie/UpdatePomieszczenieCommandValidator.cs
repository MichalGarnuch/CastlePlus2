using FluentValidation;

namespace CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.UpdatePomieszczenie
{
    public sealed class UpdatePomieszczenieCommandValidator : AbstractValidator<UpdatePomieszczenieCommand>
    {
        public UpdatePomieszczenieCommandValidator()
        {
            // Rule from SQL: IdEncji uniqueidentifier NOT NULL
            RuleFor(x => x.Id)
                .NotEmpty();

            // Rule from SQL: IdEncjiNadrzednej uniqueidentifier NOT NULL
            RuleFor(x => x.IdEncjiNadrzednej)
                .NotEmpty();

            // Rule from SQL: KodPomieszczenia nvarchar(50) NOT NULL
            RuleFor(x => x.KodPomieszczenia)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}