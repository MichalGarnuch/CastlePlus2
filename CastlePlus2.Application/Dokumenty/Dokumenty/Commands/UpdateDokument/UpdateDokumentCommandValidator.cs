using FluentValidation;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Commands.UpdateDokument
{
    public sealed class UpdateDokumentCommandValidator : AbstractValidator<UpdateDokumentCommand>
    {
        public UpdateDokumentCommandValidator()
        {
            // Rule from SQL: IdDokumentu bigint NOT NULL
            RuleFor(x => x.IdDokumentu)
                .NotEmpty();

            // Rule from SQL: Nazwa nvarchar(200) NOT NULL
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(200);

            // Rule from SQL: Opis nvarchar(500) NULL
            RuleFor(x => x.Opis)
                .MaximumLength(500);

            // Rule from SQL: SciezkaPliku nvarchar(400) NOT NULL
            RuleFor(x => x.SciezkaPliku)
                .NotEmpty()
                .MaximumLength(400);
        }
    }
}