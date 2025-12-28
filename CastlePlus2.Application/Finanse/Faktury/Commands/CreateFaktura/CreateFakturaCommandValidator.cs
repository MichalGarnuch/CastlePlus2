using FluentValidation;

namespace CastlePlus2.Application.Finanse.Faktury.Commands.CreateFaktura
{
    public sealed class CreateFakturaCommandValidator : AbstractValidator<CreateFakturaCommand>
    {
        public CreateFakturaCommandValidator()
        {
            // Rule from SQL: NumerFaktury nvarchar(60) NOT NULL
            RuleFor(x => x.NumerFaktury)
                .NotEmpty()
                .MaximumLength(60);

            // Rule from SQL: IdPodmiotu bigint NOT NULL
            RuleFor(x => x.IdPodmiotu)
                .NotEmpty();

            // Rule from SQL: DataWystawienia date NOT NULL
            RuleFor(x => x.DataWystawienia)
                .NotEmpty();

            // Rule from SQL: KodWaluty char(3) NOT NULL
            RuleFor(x => x.KodWaluty)
                .NotEmpty()
                .MaximumLength(3);
        }
    }
}