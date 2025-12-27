using FluentValidation;

namespace CastlePlus2.Application.Media.Liczniki.Commands.UpdateLicznik
{
    public sealed class UpdateLicznikCommandValidator : AbstractValidator<UpdateLicznikCommand>
    {
        public UpdateLicznikCommandValidator()
        {
            // Rule from SQL: IdLicznika bigint NOT NULL
            RuleFor(x => x.IdLicznika)
                .NotEmpty();

            // Rule from SQL: IdPrzylacza bigint NOT NULL
            RuleFor(x => x.IdPrzylacza)
                .NotEmpty();

            // Rule from SQL: NumerNV nvarchar(60) NOT NULL
            RuleFor(x => x.NumerNV)
                .NotEmpty()
                .MaximumLength(60);

            // Rule from SQL: KodJednostki nvarchar(20) NOT NULL
            RuleFor(x => x.KodJednostki)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}