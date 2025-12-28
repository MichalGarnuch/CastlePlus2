using FluentValidation;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.UpdateRozliczeniePlatnosci
{
    public sealed class UpdateRozliczeniePlatnosciCommandValidator : AbstractValidator<UpdateRozliczeniePlatnosciCommand>
    {
        public UpdateRozliczeniePlatnosciCommandValidator()
        {
            // Rule from SQL: IdRozliczenia bigint NOT NULL
            RuleFor(x => x.IdRozliczenia)
                .NotEmpty();

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