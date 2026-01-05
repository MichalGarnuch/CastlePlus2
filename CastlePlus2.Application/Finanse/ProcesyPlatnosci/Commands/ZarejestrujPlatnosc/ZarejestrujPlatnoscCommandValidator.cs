using FluentValidation;

namespace CastlePlus2.Application.Finanse.ProcesyPlatnosci.Commands.ZarejestrujPlatnosc
{
    public sealed class ZarejestrujPlatnoscCommandValidator : AbstractValidator<ZarejestrujPlatnoscCommand>
    {
        public ZarejestrujPlatnoscCommandValidator()
        {
            RuleFor(x => x.IdPodmiotu)
                .GreaterThan(0);

            RuleFor(x => x.DataPlatnosci)
                .NotEmpty();

            RuleFor(x => x.KodWaluty)
                .NotEmpty()
                .Length(3);

            RuleFor(x => x.Kwota)
                .GreaterThan(0);

            RuleFor(x => x.Rozliczenia)
                .NotEmpty();

            RuleForEach(x => x.Rozliczenia).ChildRules(rozliczenie =>
            {
                rozliczenie.RuleFor(x => x.IdFaktury).GreaterThan(0);
                rozliczenie.RuleFor(x => x.Kwota).GreaterThan(0);
            });
        }
    }
}