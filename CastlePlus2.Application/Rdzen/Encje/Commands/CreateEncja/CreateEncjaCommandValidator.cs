using FluentValidation;

namespace CastlePlus2.Application.Rdzen.Encje.Commands.CreateEncja
{
    public sealed class CreateEncjaCommandValidator : AbstractValidator<CreateEncjaCommand>
    {
        public CreateEncjaCommandValidator()
        {
            // Rule from SQL: TypEncji nvarchar(40) NOT NULL
            RuleFor(x => x.TypEncji)
                .NotEmpty()
                .MaximumLength(40);

            // Rule from SQL: KodEncji nvarchar(40) NULL
            RuleFor(x => x.KodEncji)
                .MaximumLength(40);
        }
    }
}