using FluentValidation;

namespace CastlePlus2.Application.Media.Przylacza.Commands.CreatePrzylacze
{
    public sealed class CreatePrzylaczeCommandValidator : AbstractValidator<CreatePrzylaczeCommand>
    {
        public CreatePrzylaczeCommandValidator()
        {
            // Rule from SQL: IdEncjiGospodarza uniqueidentifier NOT NULL
            RuleFor(x => x.IdEncjiGospodarza)
                .NotEmpty();

            // Rule from SQL: KodRodzaju nvarchar(20) NOT NULL
            RuleFor(x => x.KodRodzaju)
                .NotEmpty()
                .MaximumLength(20);

            // Rule from SQL: Opis nvarchar(200) NULL
            RuleFor(x => x.Opis)
                .MaximumLength(200);
        }
    }
}