using FluentValidation;

namespace CastlePlus2.Application.Media.RodzajeMediow.Commands.UpdateRodzajMedium
{
    public sealed class UpdateRodzajMediumCommandValidator : AbstractValidator<UpdateRodzajMediumCommand>
    {
        public UpdateRodzajMediumCommandValidator()
        {
            // Rule from SQL: KodRodzaju nvarchar(20) NOT NULL
            RuleFor(x => x.KodRodzaju)
                .NotEmpty()
                .MaximumLength(20);

            // Rule from SQL: Nazwa nvarchar(100) NOT NULL
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}