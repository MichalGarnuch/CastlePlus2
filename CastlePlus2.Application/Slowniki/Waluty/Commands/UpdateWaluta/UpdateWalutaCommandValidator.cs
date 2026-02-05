using FluentValidation;

namespace CastlePlus2.Application.Slowniki.Waluty.Commands.UpdateWaluta
{
    public sealed class UpdateWalutaCommandValidator : AbstractValidator<UpdateWalutaCommand>
    {
        public UpdateWalutaCommandValidator()
        {
            // Rule from SQL: slowniki.Waluta.KodWaluty char(3) NOT NULL
            RuleFor(x => x.KodWaluty)
                .NotEmpty()
                .MaximumLength(3);

            // Rule from SQL: slowniki.Waluta.Nazwa nvarchar(50) NOT NULL
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}