using FluentValidation;

namespace CastlePlus2.Application.Dokumenty.Rejestracja.Commands.RegisterDokument
{
    public class RegisterDokumentCommandValidator : AbstractValidator<RegisterDokumentCommand>
    {
        public RegisterDokumentCommandValidator()
        {
            RuleFor(x => x.IdEncji)
                .NotEmpty();

            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Opis)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Opis));

            RuleFor(x => x.SciezkaPliku)
                .MaximumLength(400)
                .When(x => !string.IsNullOrWhiteSpace(x.SciezkaPliku));
        }
    }
}