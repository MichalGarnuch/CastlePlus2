using FluentValidation;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Commands.CreateUmowaNajmu
{
    public sealed class CreateUmowaNajmuCommandValidator : AbstractValidator<CreateUmowaNajmuCommand>
    {
        public CreateUmowaNajmuCommandValidator()
        {
            RuleFor(x => x.IdWynajmujacego)
                .NotEmpty();

            RuleFor(x => x.IdNajemcy)
                .NotEmpty();

            RuleFor(x => x.DataZawarcia)
                .NotEmpty();

            RuleFor(x => x.DataPoczatku)
                .NotEmpty();

            RuleFor(x => x.KodWaluty)
                .NotEmpty()
                .MaximumLength(3);

            RuleFor(x => x.KodIndeksacji)
                .MaximumLength(20);
        }
    }
}