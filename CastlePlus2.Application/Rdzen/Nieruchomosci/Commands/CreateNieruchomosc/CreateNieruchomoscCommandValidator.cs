using FluentValidation;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.CreateNieruchomosc
{
    public sealed class CreateNieruchomoscCommandValidator : AbstractValidator<CreateNieruchomoscCommand>
    {
        public CreateNieruchomoscCommandValidator()
        {
            // Rule from SQL: Nazwa nvarchar(200) NOT NULL
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}