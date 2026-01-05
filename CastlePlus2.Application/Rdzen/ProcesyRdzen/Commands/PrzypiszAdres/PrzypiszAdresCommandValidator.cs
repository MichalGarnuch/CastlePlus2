using FluentValidation;

namespace CastlePlus2.Application.Rdzen.ProcesyRdzen.Commands.PrzypiszAdres
{
    public sealed class PrzypiszAdresCommandValidator : AbstractValidator<PrzypiszAdresCommand>
    {
        public PrzypiszAdresCommandValidator()
        {
            RuleFor(x => x.IdEncji)
                .NotEmpty();

            RuleFor(x => x.IdAdresu)
                .NotEmpty();

            RuleFor(x => x.OdDnia)
                .NotEmpty();

            RuleFor(x => x.DoDnia)
                .Must((cmd, doDnia) => doDnia == null || doDnia.Value >= cmd.OdDnia)
                .WithMessage("DoDnia nie może być wcześniejsze niż OdDnia.");
        }
    }
}