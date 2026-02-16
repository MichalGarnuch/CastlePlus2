using CastlePlus2.Contracts.Exports;
using FluentValidation;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.GenerateFakturaWydruk
{
    public class GenerateFakturaWydrukCommandValidator : AbstractValidator<GenerateFakturaWydrukCommand>
    {
        public GenerateFakturaWydrukCommandValidator()
        {
            RuleFor(x => x.IdFaktury)
                .GreaterThan(0); // Rule from SQL: [finanse].[Faktura].[IdFaktury] is PK bigint IDENTITY > 0.

            RuleFor(x => x.TemplateDokumentId)
                .GreaterThan(0); // Rule from SQL: [dokumenty].[Dokument].[IdDokumentu] is PK bigint IDENTITY > 0.

            RuleFor(x => x.Format)
                .Equal(ExportFormat.Docx)
                .WithMessage("Obsługiwany format wydruku faktury to wyłącznie DOCX."); // Rule from SQL/process: szablony przechowujemy jako pliki DOCX.
        }
    }
}