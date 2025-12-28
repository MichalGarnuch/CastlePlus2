using FluentValidation;

namespace CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Commands.CreatePowiazanieDokumentu
{
    public sealed class CreatePowiazanieDokumentuCommandValidator : AbstractValidator<CreatePowiazanieDokumentuCommand>
    {
        public CreatePowiazanieDokumentuCommandValidator()
        {
            // Rule from SQL: IdDokumentu bigint NOT NULL
            RuleFor(x => x.IdDokumentu)
                .NotEmpty();

            // Rule from SQL: IdEncji uniqueidentifier NOT NULL
            RuleFor(x => x.IdEncji)
                .NotEmpty();
        }
    }
}