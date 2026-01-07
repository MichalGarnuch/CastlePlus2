using CastlePlus2.Application.Interfaces.Exports;
using CastlePlus2.Contracts.Exports;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Raporty
{
    [ApiController]
    [Route("api/raporty")]
    public class RaportyController : ControllerBase
    {
        private readonly IReportExportService _reportExportService;

        public RaportyController(IReportExportService reportExportService)
        {
            _reportExportService = reportExportService;
        }

        [HttpGet("eksport")]
        public IActionResult Export([FromQuery] string reportKey, [FromQuery] ExportFormat format)
        {
            var rows = GetReportRows(reportKey);
            if (rows is null)
            {
                return BadRequest("Nieznany raport do eksportu.");
            }

            var fileNameBase = $"{reportKey}_{DateTime.UtcNow:yyyyMMdd_HHmm}";

            return format switch
            {
                ExportFormat.Csv => File(
                    _reportExportService.ExportCsv(rows, fileNameBase),
                    "text/csv",
                    $"{fileNameBase}.csv"),
                ExportFormat.Pdf => File(
                    _reportExportService.ExportPdf(rows, $"Raport: {reportKey}", fileNameBase),
                    "application/pdf",
                    $"{fileNameBase}.pdf"),
                _ => BadRequest("Nieobsługiwany format eksportu.")
            };
        }

        private static IReadOnlyList<ReportRow>? GetReportRows(string reportKey)
        {
            return reportKey switch
            {
                "podsumowanie" => new List<ReportRow>
                {
                    new("Aktywni użytkownicy", "128"),
                    new("Liczba umów", "64"),
                    new("Otwarte zgłoszenia", "12")
                },
                "faktury" => new List<ReportRow>
                {
                    new("Faktury wystawione", "24"),
                    new("Faktury opłacone", "18"),
                    new("Faktury zaległe", "6")
                },
                _ => null
            };
        }

        private sealed record ReportRow(string Name, string Value);
    }
}