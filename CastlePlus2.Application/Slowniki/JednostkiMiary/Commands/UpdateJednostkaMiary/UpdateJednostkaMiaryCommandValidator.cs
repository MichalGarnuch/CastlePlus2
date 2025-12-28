using FluentValidation;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.UpdateJednostkaMiary
{
    public sealed class UpdateJednostkaMiaryCommandValidator : AbstractValidator<UpdateJednostkaMiaryCommand>
    {
        public UpdateJednostkaMiaryCommandValidator()
        {
            // Reguła z SQL: KodJednostki nvarchar(20) NOT NULL
            RuleFor(x => x.KodJednostki)
                .NotEmpty()
                .MaximumLength(20);

            // Reguła z SQL: Nazwa nvarchar(100) NOT NULL
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}