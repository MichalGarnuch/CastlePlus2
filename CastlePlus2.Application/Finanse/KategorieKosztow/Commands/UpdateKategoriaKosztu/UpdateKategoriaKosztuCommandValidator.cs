using FluentValidation;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Commands.UpdateKategoriaKosztu
{
    public sealed class UpdateKategoriaKosztuCommandValidator : AbstractValidator<UpdateKategoriaKosztuCommand>
    {
        public UpdateKategoriaKosztuCommandValidator()
        {
            // Reguła z SQL: IdKategoriiKosztu bigint NOT NULL
            RuleFor(x => x.IdKategoriiKosztu)
                .NotEmpty();

            // Reguła z SQL: Kod nvarchar(20) NOT NULL
            RuleFor(x => x.Kod)
                .NotEmpty()
                .MaximumLength(20);

            // Reguła z SQL: Nazwa nvarchar(100) NOT NULL
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}