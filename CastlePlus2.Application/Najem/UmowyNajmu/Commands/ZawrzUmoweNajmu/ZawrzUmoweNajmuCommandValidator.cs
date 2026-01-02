using FluentValidation;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Commands.ZawrzUmoweNajmu
{
    public sealed class ZawrzUmoweNajmuCommandValidator : AbstractValidator<ZawrzUmoweNajmuCommand>
    {
        public ZawrzUmoweNajmuCommandValidator()
        {
            RuleFor(x => x.IdLokalu)
                .NotEmpty();

            RuleFor(x => x.IdWynajmujacego)
                .NotEmpty();

            RuleFor(x => x.IdNajemcy)
                .NotEmpty();

            RuleFor(x => x.DataZawarcia)
                .NotEmpty();

            RuleFor(x => x.DataPoczatku)
                .NotEmpty();

            RuleFor(x => x.DataZakonczenia)
                .Must((cmd, data) => data is null || data > cmd.DataPoczatku)
                .WithMessage("Data zakończenia musi być późniejsza niż data początku.");

            RuleFor(x => x.KodEncji)
                .MaximumLength(40);

            RuleFor(x => x.KodWaluty)
                .NotEmpty()
                .Length(3);

            RuleFor(x => x.KodIndeksacji)
                .MaximumLength(20);

            RuleFor(x => x.NazwaCzynszu)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.KodJednostki)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Stawka)
                .NotEmpty();

            RuleFor(x => x.KwotaKaucji)
                .GreaterThanOrEqualTo(0)
                .When(x => x.KwotaKaucji.HasValue);

            RuleFor(x => x.UdzialProcent)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .When(x => x.UdzialProcent.HasValue);
        }
    }
}