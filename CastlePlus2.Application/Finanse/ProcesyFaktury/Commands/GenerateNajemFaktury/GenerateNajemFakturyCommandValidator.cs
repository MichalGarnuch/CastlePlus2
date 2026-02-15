using FluentValidation;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.GenerateNajemFaktury
{
    public sealed class GenerateNajemFakturyCommandValidator : AbstractValidator<GenerateNajemFakturyCommand>
    {
        public GenerateNajemFakturyCommandValidator()
        {
            RuleFor(x => x.Miesiac)
                .NotEmpty()
                .Matches("^\\d{4}-(0[1-9]|1[0-2])$")
                .WithMessage("Miesiąc musi być w formacie YYYY-MM."); // Rule from SQL: okres fakturowania to miesiąc kalendarzowy.

            RuleFor(x => x.DataWystawienia)
                .NotEmpty(); // Rule from SQL: [finanse].[Faktura].[DataWystawienia] is NOT NULL.
        }
    }
}