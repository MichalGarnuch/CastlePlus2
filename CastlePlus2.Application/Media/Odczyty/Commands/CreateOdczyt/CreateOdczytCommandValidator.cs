using FluentValidation;

namespace CastlePlus2.Application.Media.Odczyty.Commands.CreateOdczyt
{
    public sealed class CreateOdczytCommandValidator : AbstractValidator<CreateOdczytCommand>
    {
        public CreateOdczytCommandValidator()
        {
            // Reguła z SQL: IdLicznika bigint NOT NULL
            RuleFor(x => x.IdLicznika)
                .NotEmpty();

            // Reguła z SQL: DataOdczytu date NOT NULL
            RuleFor(x => x.DataOdczytu)
                .NotEmpty();

            // Reguła z SQL: Wskazanie decimal(18,6) NOT NULL
            RuleFor(x => x.Wskazanie)
                .NotEmpty();

            // Reguła z SQL: Zrodlo nvarchar(20) NULL
            RuleFor(x => x.Zrodlo)
                .MaximumLength(20);
        }
    }
}