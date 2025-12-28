using FluentValidation;

namespace CastlePlus2.Application.Finanse.Platnosci.Commands.UpdatePlatnosc
{
    public sealed class UpdatePlatnoscCommandValidator : AbstractValidator<UpdatePlatnoscCommand>
    {
        public UpdatePlatnoscCommandValidator()
        {
            // Rule from SQL: IdPlatnosci bigint NOT NULL
            RuleFor(x => x.IdPlatnosci)
                .NotEmpty();

            // Rule from SQL: IdPodmiotu bigint NOT NULL
            RuleFor(x => x.IdPodmiotu)
                .NotEmpty();

            // Rule from SQL: DataPlatnosci date NOT NULL
            RuleFor(x => x.DataPlatnosci)
                .NotEmpty();

            // Rule from SQL: KodWaluty char(3) NOT NULL
            RuleFor(x => x.KodWaluty)
                .NotEmpty()
                .MaximumLength(3);

            // Rule from SQL: Kwota decimal(18,2) NOT NULL
            RuleFor(x => x.Kwota)
                .NotEmpty();
        }
    }
}