using FluentValidation;

namespace CastlePlus2.Application.Najem.Kaucje.Commands.CreateKaucja
{
    public sealed class CreateKaucjaCommandValidator : AbstractValidator<CreateKaucjaCommand>
    {
        public CreateKaucjaCommandValidator()
        {
            // Reguła z SQL: IdUmowyNajmu uniqueidentifier NOT NULL
            RuleFor(x => x.IdUmowyNajmu)
                .NotEmpty();

            // Reguła z SQL: RodzajOperacji nvarchar(20) NOT NULL
            RuleFor(x => x.RodzajOperacji)
                .NotEmpty()
                .MaximumLength(20);

            // Reguła z SQL: Kwota decimal(12,2) NOT NULL
            RuleFor(x => x.Kwota)
                .NotEmpty();

            // Reguła z SQL: KodWaluty char(3) NOT NULL
            RuleFor(x => x.KodWaluty)
                .NotEmpty()
                .Length(3);

            // Reguła z SQL: DataOperacji date NOT NULL
            RuleFor(x => x.DataOperacji)
                .NotEmpty();
        }
    }
}