using FluentValidation;

namespace CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Commands.UpdatePowiazanieDokumentu
{
    public sealed class UpdatePowiazanieDokumentuCommandValidator : AbstractValidator<UpdatePowiazanieDokumentuCommand>
    {
        public UpdatePowiazanieDokumentuCommandValidator()
        {
            // Rule from SQL: IdPowiazania bigint NOT NULL
            RuleFor(x => x.IdPowiazania)
                .NotEmpty();

            // Rule from SQL: IdDokumentu bigint NOT NULL
            RuleFor(x => x.IdDokumentu)
                .NotEmpty();

            // Rule from SQL: IdEncji uniqueidentifier NOT NULL
            RuleFor(x => x.IdEncji)
                .NotEmpty();
        }
    }
}