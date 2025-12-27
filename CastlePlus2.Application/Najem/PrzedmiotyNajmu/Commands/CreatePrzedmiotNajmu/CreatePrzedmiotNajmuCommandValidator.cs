using FluentValidation;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.CreatePrzedmiotNajmu
{
    public sealed class CreatePrzedmiotNajmuCommandValidator : AbstractValidator<CreatePrzedmiotNajmuCommand>
    {
        public CreatePrzedmiotNajmuCommandValidator()
        {
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