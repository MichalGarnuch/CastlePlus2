using FluentValidation;

namespace CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Commands.UpdatePrzypisanieAdresu
{
    public sealed class UpdatePrzypisanieAdresuCommandValidator : AbstractValidator<UpdatePrzypisanieAdresuCommand>
    {
        public UpdatePrzypisanieAdresuCommandValidator()
        {
            // Rule from SQL: IdPrzypisaniaAdresu bigint NOT NULL
            RuleFor(x => x.IdPrzypisaniaAdresu)
                .NotEmpty();

            // Rule from SQL: IdEncji uniqueidentifier NOT NULL
            RuleFor(x => x.IdEncji)
                .NotEmpty();

            // Rule from SQL: IdAdresu bigint NOT NULL
            RuleFor(x => x.IdAdresu)
                .NotEmpty();

            // Rule from SQL: OdDnia date NOT NULL
            RuleFor(x => x.OdDnia)
                .NotEmpty();
        }
    }
}