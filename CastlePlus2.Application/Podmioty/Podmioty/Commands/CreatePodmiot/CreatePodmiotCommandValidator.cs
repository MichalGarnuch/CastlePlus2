using FluentValidation;

namespace CastlePlus2.Application.Podmioty.Podmioty.Commands.CreatePodmiot
{
    public sealed class CreatePodmiotCommandValidator : AbstractValidator<CreatePodmiotCommand>
    {
        public CreatePodmiotCommandValidator()
        {
            // Rule from SQL: Nazwa nvarchar(200) NOT NULL
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(200);

            // Rule from SQL: NIP nvarchar(20) NULL
            RuleFor(x => x.NIP)
                .MaximumLength(20);

            // Rule from SQL: REGON nvarchar(20) NULL
            RuleFor(x => x.REGON)
                .MaximumLength(20);

            // Rule from SQL: PESEL nvarchar(11) NULL
            RuleFor(x => x.PESEL)
                .MaximumLength(11);

            // Rule from SQL: TypPodmiotu nvarchar(30) NOT NULL
            RuleFor(x => x.TypPodmiotu)
                .NotEmpty()
                .MaximumLength(30);
        }
    }
}