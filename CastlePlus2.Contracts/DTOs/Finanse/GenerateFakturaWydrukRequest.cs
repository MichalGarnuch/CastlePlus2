using CastlePlus2.Contracts.Exports;

namespace CastlePlus2.Contracts.Requests.Finanse
{
    public class GenerateFakturaWydrukRequest
    {
        public long IdFaktury { get; set; }
        public long TemplateDokumentId { get; set; }
        public ExportFormat Format { get; set; } = ExportFormat.Docx;
        public bool IncludeAllocations { get; set; }
    }
}