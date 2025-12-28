using FluentValidation;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.CreateZleceniePracy
{
    public sealed class CreateZleceniePracyCommandValidator : AbstractValidator<CreateZleceniePracyCommand>
    {
        public CreateZleceniePracyCommandValidator()
        {
            // Reguła z SQL: IdEncjiGospodarza uniqueidentifier NOT NULL
            RuleFor(x => x.IdEncjiGospodarza)
                .NotEmpty();

            // Reguła z SQL: Tytul nvarchar(200) NOT NULL
            RuleFor(x => x.Tytul)
                .NotEmpty()
                .MaximumLength(200);

            // Reguła z SQL: Opis nvarchar(1000) NULL
            RuleFor(x => x.Opis)
                .MaximumLength(1000);

            // Reguła z SQL: Status nvarchar(20) NOT NULL
            RuleFor(x => x.Status)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}