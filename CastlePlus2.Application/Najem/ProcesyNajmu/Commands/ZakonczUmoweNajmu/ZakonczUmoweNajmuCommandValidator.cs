using FluentValidation;

namespace CastlePlus2.Application.Najem.ProcesyNajmu.Commands.ZakonczUmoweNajmu
{
    public sealed class ZakonczUmoweNajmuCommandValidator : AbstractValidator<ZakonczUmoweNajmuCommand>
    {
        public ZakonczUmoweNajmuCommandValidator()
        {
            RuleFor(x => x.IdUmowyNajmu)
                .NotEmpty();

            RuleFor(x => x.DataZakonczenia)
                .NotEmpty();

            RuleFor(x => x.KwotaZwrotuKaucji)
                .GreaterThan(0)
                .When(x => x.KwotaZwrotuKaucji.HasValue);
        }
    }
}