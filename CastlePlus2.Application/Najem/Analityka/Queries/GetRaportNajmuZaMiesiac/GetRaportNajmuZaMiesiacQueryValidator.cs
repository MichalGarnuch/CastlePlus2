using FluentValidation;

namespace CastlePlus2.Application.Najem.Analityka.Queries.GetRaportNajmuZaMiesiac
{
    public sealed class GetRaportNajmuZaMiesiacQueryValidator : AbstractValidator<GetRaportNajmuZaMiesiacQuery>
    {
        public GetRaportNajmuZaMiesiacQueryValidator()
        {
            // Rule from SQL: najem.usp_RaportNajmuZaMiesiac @Rok int + DATEFROMPARTS(@Rok, @Miesiac, 1)
            RuleFor(x => x.Request.Rok)
                .InclusiveBetween(1900, 9999)
                .WithMessage("Rok musi być w zakresie 1900-9999.");

            // Rule from SQL: najem.usp_RaportNajmuZaMiesiac @Miesiac int + DATEFROMPARTS(@Rok, @Miesiac, 1)
            RuleFor(x => x.Request.Miesiac)
                .InclusiveBetween(1, 12)
                .WithMessage("Miesiąc musi być w zakresie 1-12.");
        }
    }
}