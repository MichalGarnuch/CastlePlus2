using FluentValidation;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Commands.CreateDokument
{
    public sealed class CreateDokumentCommandValidator : AbstractValidator<CreateDokumentCommand>
    {
        public CreateDokumentCommandValidator()
        {
            // Rule from SQL: Nazwa nvarchar(200) NOT NULL
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(200);

            // Rule from SQL: Opis nvarchar(500) NULL
            RuleFor(x => x.Opis)
                .MaximumLength(500);

            // Rule from SQL: SciezkaPliku nvarchar(400) NOT NULL
            RuleFor(x => x.SciezkaPliku)
                .NotEmpty()
                .MaximumLength(400);
        }
    }
}