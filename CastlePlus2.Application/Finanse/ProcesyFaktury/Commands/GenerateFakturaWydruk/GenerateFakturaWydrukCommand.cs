using CastlePlus2.Contracts.Exports;
using MediatR;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.GenerateFakturaWydruk
{
    public class GenerateFakturaWydrukCommand : IRequest<GenerateFakturaWydrukCommandResult>
    {
        public long IdFaktury { get; set; }
        public long TemplateDokumentId { get; set; }
        public ExportFormat Format { get; set; } = ExportFormat.Docx;
        public bool IncludeAllocations { get; set; }
    }

    public class GenerateFakturaWydrukCommandResult
    {
        public byte[] Bytes { get; set; } = Array.Empty<byte>();
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public List<string> Warnings { get; set; } = new();
    }
}
