using FluentValidation;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Commands.UpdateZasobUITekst;

public sealed class UpdateZasobUITekstCommandValidator : AbstractValidator<UpdateZasobUITekstCommand>
{
    public UpdateZasobUITekstCommandValidator()
    {
        RuleFor(x => x.IdZasobuTekstu)
            .GreaterThan(0);

        RuleFor(x => x.IdEncji)
            .NotEmpty();

        RuleFor(x => x.Jezyk)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(x => x.Pole)
            .NotEmpty()
            .MaximumLength(40);

        RuleFor(x => x.Wartosc)
            .NotEmpty();

        RuleFor(x => x.Format)
            .NotEmpty()
            .MaximumLength(20);
    }
}
