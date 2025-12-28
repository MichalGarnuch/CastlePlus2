using FluentValidation;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.CreateRozliczeniePlatnosci
{
    public sealed class CreateRozliczeniePlatnosciCommandValidator : AbstractValidator<CreateRozliczeniePlatnosciCommand>
    {
        public CreateRozliczeniePlatnosciCommandValidator()
        {
            // Rule from SQL: IdPlatnosci bigint NOT NULL
            RuleFor(x => x.IdPlatnosci)
                .NotEmpty();

            // Rule from SQL: IdFaktury bigint NOT NULL
            RuleFor(x => x.IdFaktury)
                .NotEmpty();

            // Rule from SQL: Kwota decimal(18, 2) NOT NULL
            RuleFor(x => x.Kwota)
                .NotEmpty();
        }
    }
}