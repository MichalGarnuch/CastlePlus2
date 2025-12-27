using FluentValidation;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.UpdatePrzedmiotNajmu
{
    public sealed class UpdatePrzedmiotNajmuCommandValidator : AbstractValidator<UpdatePrzedmiotNajmuCommand>
    {
        public UpdatePrzedmiotNajmuCommandValidator()
        {
            // Rule from SQL: IdPrzedmiotuNajmu bigint NOT NULL
            RuleFor(x => x.IdPrzedmiotuNajmu)
                .NotEmpty();

            // Rule from SQL: IdUmowyNajmu uniqueidentifier NOT NULL
            RuleFor(x => x.IdUmowyNajmu)
                .NotEmpty();

            // Rule from SQL: IdEncji uniqueidentifier NOT NULL
            RuleFor(x => x.IdEncji)
                .NotEmpty();

            // Rule from SQL: OdDnia date NOT NULL
            RuleFor(x => x.OdDnia)
                .NotEmpty();
        }
    }
}