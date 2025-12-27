using FluentValidation;

namespace CastlePlus2.Application.Rdzen.Adresy.Commands.CreateAdres
{
    public sealed class CreateAdresCommandValidator : AbstractValidator<CreateAdresCommand>
    {
        public CreateAdresCommandValidator()
        {
            // Rule from SQL: Kraj nvarchar(60) NOT NULL
            RuleFor(x => x.Kraj)
                .NotEmpty()
                .MaximumLength(60);

            // Rule from SQL: Wojewodztwo nvarchar(60) NULL
            RuleFor(x => x.Wojewodztwo)
                .MaximumLength(60);

            // Rule from SQL: Powiat nvarchar(60) NULL
            RuleFor(x => x.Powiat)
                .MaximumLength(60);

            // Rule from SQL: Gmina nvarchar(60) NULL
            RuleFor(x => x.Gmina)
                .MaximumLength(60);

            // Rule from SQL: Miejscowosc nvarchar(100) NOT NULL
            RuleFor(x => x.Miejscowosc)
                .NotEmpty()
                .MaximumLength(100);

            // Rule from SQL: Ulica nvarchar(100) NULL
            RuleFor(x => x.Ulica)
                .MaximumLength(100);

            // Rule from SQL: Numer nvarchar(20) NULL
            RuleFor(x => x.Numer)
                .MaximumLength(20);

            // Rule from SQL: KodPocztowy nvarchar(12) NULL
            RuleFor(x => x.KodPocztowy)
                .MaximumLength(12);

            // Rule from SQL: AdresPelny nvarchar(300) NULL
            RuleFor(x => x.AdresPelny)
                .MaximumLength(300);
        }
    }
}