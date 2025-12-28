using FluentValidation;

namespace CastlePlus2.Application.Najem.SkladnikiCzynszu.Commands.CreateSkladnikCzynszu
{
    public sealed class CreateSkladnikCzynszuCommandValidator : AbstractValidator<CreateSkladnikCzynszuCommand>
    {
        public CreateSkladnikCzynszuCommandValidator()
        {
            // Reguła z SQL: IdUmowyNajmu uniqueidentifier NOT NULL
            RuleFor(x => x.IdUmowyNajmu)
                .NotEmpty();

            // Reguła z SQL: Nazwa nvarchar(100) NOT NULL
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(100);

            // Reguła z SQL: KodJednostki nvarchar(20) NOT NULL
            RuleFor(x => x.KodJednostki)
                .NotEmpty()
                .MaximumLength(20);

            // Reguła z SQL: Stawka decimal(12,4) NOT NULL
            RuleFor(x => x.Stawka)
                .NotEmpty();

            // Reguła z SQL: KodIndeksacji nvarchar(20) NULL
            RuleFor(x => x.KodIndeksacji)
                .MaximumLength(20)
                .When(x => x.KodIndeksacji is not null);

            // Reguła z SQL: OdDnia date NOT NULL
            RuleFor(x => x.OdDnia)
                .NotEmpty();
        }
    }
}