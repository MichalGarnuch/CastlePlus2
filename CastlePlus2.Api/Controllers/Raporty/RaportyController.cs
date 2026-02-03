using CastlePlus2.Application.Interfaces.Exports;
using CastlePlus2.Application.Interfaces.Reports;
using CastlePlus2.Contracts.Exports;
using CastlePlus2.Infrastructure.Services.Reports.Definitions;
using CastlePlus2.Shared.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CastlePlus2.Api.Controllers.Raporty
{
    [ApiController]
    [Route("api/raporty")]
    [Authorize]
    public class RaportyController : ControllerBase
    {
        private readonly IReportExportService _reportExportService;
        private readonly IReportRegistry _reportRegistry;
        private readonly IExportArchiveService _exportArchiveService;
        private readonly IReportDataPreviewService _reportDataPreviewService;
        private readonly IReportDocumentPreviewService _reportDocumentPreviewService;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        public RaportyController(
            IReportExportService reportExportService,
            IReportRegistry reportRegistry,
            IExportArchiveService exportArchiveService,
            IReportDataPreviewService reportDataPreviewService,
            IReportDocumentPreviewService reportDocumentPreviewService,
            IMemoryCache cache,
            IConfiguration configuration)
        {
            _reportExportService = reportExportService;
            _reportRegistry = reportRegistry;
            _exportArchiveService = exportArchiveService;
            _reportDataPreviewService = reportDataPreviewService;
            _reportDocumentPreviewService = reportDocumentPreviewService;
            _cache = cache;
            _configuration = configuration;
        }

        // =========================================================
        // 1️⃣ GENEROWANIE LINKU DO EKSPORTU (AUTH REQUIRED)
        // =========================================================
        [HttpGet("eksport-link")]
        public async Task<IActionResult> CreateExportLink(
            [FromQuery] string reportKey,
            [FromQuery] ExportFormat format,
            [FromQuery] bool archive = false,
            [FromQuery] int take = 50,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(reportKey))
                return BadRequest("Brak klucza raportu.");

            if (!Enum.IsDefined(format))
                return BadRequest("Nieobsługiwany format.");

            if (archive)
            {
                if (!User.IsInRole(RoleCodes.Admin))
                    return Forbid();
            }

            IReportDefinition definition;
            try
            {
                definition = _reportRegistry.GetByKey(reportKey);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            var rows = definition is FakturyReportDefinition faktury
                ? await faktury.BuildRowsAsync(take, ct)
                : await definition.BuildRowsAsync(ct);

            var typedRows = CreateTypedRows(definition.RowType, rows);

            var fileNameBase = $"{definition.FileNameBase}_{DateTime.UtcNow:yyyyMMdd_HHmm}";
            var fileName = fileNameBase + format.GetFileExtension();
            var contentType = format.GetContentType();

            var bytes = format switch
            {
                ExportFormat.Csv => InvokeExport(nameof(IReportExportService.ExportCsv), definition.RowType, typedRows, fileNameBase),
                ExportFormat.Pdf => InvokeExport(nameof(IReportExportService.ExportPdf), definition.RowType, typedRows, definition.Title, fileNameBase),
                ExportFormat.Xlsx => InvokeExport(nameof(IReportExportService.ExportXlsx), definition.RowType, typedRows, definition.Title, fileNameBase),
                ExportFormat.Docx => InvokeExport(nameof(IReportExportService.ExportDocx), definition.RowType, typedRows, definition.Title, fileNameBase),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (archive || _configuration.GetValue<ExportStorageMode>("ExportStorage:Mode") == ExportStorageMode.Archive)
            {
                var now = DateTime.UtcNow;
                var path = $"Exports/{now:yyyy}/{now:MM}/{now:dd}/raporty/{reportKey}/{fileName}";
                await _exportArchiveService.SaveAsync(bytes, path, ct);
            }

            var downloadId = Guid.NewGuid().ToString("N");
            var expiresAt = DateTime.UtcNow.AddMinutes(10);

            _cache.Set(
                $"report-export:{downloadId}",
                (bytes, fileName, contentType),
                expiresAt
            );

            return Ok(new ReportExportLinkResponse(
                DownloadUrl: $"/api/raporty/eksport-pobierz/{downloadId}",
                ExpiresAtUtc: expiresAt
            ));
        }

        // =========================================================
        // 2️⃣ POBRANIE PLIKU (ALLOW ANONYMOUS)
        // =========================================================
        [HttpGet("eksport-pobierz/{id}")]
        [AllowAnonymous]
        public IActionResult Download([FromRoute] string id)
        {
            if (!_cache.TryGetValue($"report-export:{id}",
                out (byte[] Bytes, string FileName, string ContentType) entry))
                return NotFound();

            Response.Headers["Cache-Control"] = "no-store";
            return File(entry.Bytes, entry.ContentType, entry.FileName);
        }

        // =========================================================
        // 3️⃣ PODGLĄDY (jak było, ale stream anonymous)
        // =========================================================
        [HttpGet("podglad-danych")]
        public async Task<IActionResult> DataPreview(string reportKey, int take = 50, CancellationToken ct = default)
            => Ok(await _reportDataPreviewService.BuildAsync(reportKey, take, ct));

        [HttpGet("podglad-dokumentu")]
        public async Task<IActionResult> DocumentPreview(string reportKey, int take = 50, CancellationToken ct = default)
            => Ok(await _reportDocumentPreviewService.CreatePdfPreviewAsync(reportKey, take, ct));

        [HttpGet("podglad-dokumentu/{previewId}")]
        [AllowAnonymous]
        public IActionResult DocumentStream(string previewId)
        {
            if (!_cache.TryGetValue($"report-doc-preview:{previewId}",
                out (byte[] Bytes, string FileName, string ContentType) entry))
                return NotFound();

            Response.Headers["Content-Disposition"] = $"inline; filename=\"{entry.FileName}\"";
            return File(entry.Bytes, entry.ContentType);
        }

        // =========================================================
        // Helpers
        // =========================================================
        private byte[] InvokeExport(string method, Type rowType, params object[] args)
        {
            var m = typeof(IReportExportService)
                .GetMethod(method)!
                .MakeGenericMethod(rowType);

            return (byte[])m.Invoke(_reportExportService, args)!;
        }

        private static object CreateTypedRows(Type type, IReadOnlyList<object> rows)
        {
            var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type))!;
            foreach (var r in rows) list.Add(r);
            return list;
        }
    }
}
