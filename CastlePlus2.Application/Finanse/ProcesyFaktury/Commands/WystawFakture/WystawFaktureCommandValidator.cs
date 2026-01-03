using FluentValidation;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.WystawFakture
{
    public sealed class WystawFaktureCommandValidator : AbstractValidator<WystawFaktureCommand>
    {
        public WystawFaktureCommandValidator()
        {
            RuleFor(x => x.NumerFaktury)
                .NotEmpty()
                .MaximumLength(60);

            RuleFor(x => x.IdPodmiotu)
                .GreaterThan(0);

            RuleFor(x => x.DataWystawienia)
                .NotEmpty();

            RuleFor(x => x.KodWaluty)
                .NotEmpty()
                .Length(3);

            RuleFor(x => x.Pozycje)
                .NotEmpty();

            RuleForEach(x => x.Pozycje).ChildRules(pozycja =>
            {
                pozycja.RuleFor(x => x.IdKategoriiKosztu).GreaterThan(0);
                pozycja.RuleFor(x => x.KwotaNetto).GreaterThan(0);
                pozycja.RuleFor(x => x.KwotaBrutto).GreaterThan(0);
                pozycja.RuleFor(x => x.Alokacje).NotEmpty();

                pozycja.RuleForEach(x => x.Alokacje).ChildRules(alokacja =>
                {
                    alokacja.RuleFor(x => x.IdEncji).NotEmpty();
                    alokacja.RuleFor(x => x.KwotaNetto).GreaterThan(0);
                    alokacja.RuleFor(x => x.KwotaBrutto).GreaterThan(0);
                });
            });
        }
    }
}