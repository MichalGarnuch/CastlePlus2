using FluentValidation;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Commands.CreateKategoriaKosztu
{
    public sealed class CreateKategoriaKosztuCommandValidator : AbstractValidator<CreateKategoriaKosztuCommand>
    {
        public CreateKategoriaKosztuCommandValidator()
        {
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