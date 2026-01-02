using FluentValidation;

namespace CastlePlus2.Application.Utrzymanie.Zgloszenia.Commands.ZglosUsterke
{
    public class ZglosUsterkeCommandValidator : AbstractValidator<ZglosUsterkeCommand>
    {
        public ZglosUsterkeCommandValidator()
        {
            RuleFor(x => x.IdEncjiGospodarza)
                .NotEmpty();

            RuleFor(x => x.Tytul)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Opis)
                .MaximumLength(1000);
        }
    }
}