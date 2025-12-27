using FluentValidation;

namespace CastlePlus2.Application.Podmioty.Kontakty.Commands.CreateKontakt
{
    public sealed class CreateKontaktCommandValidator : AbstractValidator<CreateKontaktCommand>
    {
        public CreateKontaktCommandValidator()
        {
            // Rule from SQL: IdPodmiotu bigint NOT NULL
            RuleFor(x => x.IdPodmiotu)
                .NotEmpty();

            // Rule from SQL: Rodzaj nvarchar(30) NOT NULL
            RuleFor(x => x.Rodzaj)
                .NotEmpty()
                .MaximumLength(30);

            // Rule from SQL: Wartosc nvarchar(200) NOT NULL
            RuleFor(x => x.Wartosc)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}