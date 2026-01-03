using FluentValidation;

namespace CastlePlus2.Application.Najem.ProcesyNajmu.Commands.AneksujCzynsz
{
    public sealed class AneksujCzynszCommandValidator : AbstractValidator<AneksujCzynszCommand>
    {
        public AneksujCzynszCommandValidator()
        {
            RuleFor(x => x.IdUmowyNajmu)
                .NotEmpty();

            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.KodJednostki)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Stawka)
                .GreaterThan(0);

            RuleFor(x => x.KodIndeksacji)
                .MaximumLength(20)
                .When(x => x.KodIndeksacji is not null);

            RuleFor(x => x.OdDnia)
                .NotEmpty();
        }
    }
}