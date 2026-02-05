using FluentValidation;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.CreateJednostkaMiary
{
    public sealed class CreateJednostkaMiaryCommandValidator : AbstractValidator<CreateJednostkaMiaryCommand>
    {
        public CreateJednostkaMiaryCommandValidator()
        {
            // Rule from SQL: slowniki.JednostkaMiary.KodJednostki nvarchar(20) NOT NULL
            RuleFor(x => x.KodJednostki)
                .NotEmpty()
                .MaximumLength(20);

            // Rule from SQL: slowniki.JednostkaMiary.Nazwa nvarchar(100) NOT NULL
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}