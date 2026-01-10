using CastlePlus2.Application.Interfaces.Exports;
using CastlePlus2.Application.Interfaces.Reports;
using CastlePlus2.Contracts.Exports;
using CastlePlus2.Infrastructure.Services.Reports.Definitions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace CastlePlus2.Api.Controllers.Raporty
{
    [ApiController]
    [Route("api/raporty")]
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

        [HttpGet("eksport")]
        public async Task<IActionResult> Export(
            [FromQuery] string reportKey,
            [FromQuery] ExportFormat format,
            [FromQuery] bool archive = false,
            [FromQuery] int take = 50,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(reportKey))
            {
                return BadRequest("Brak klucza raportu.");
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

            var rowsObjects = definition is FakturyReportDefinition fakturyDefinition
                ? await fakturyDefinition.BuildRowsAsync(take, ct)
                : await definition.BuildRowsAsync(ct);

            var typedRows = CreateTypedRows(definition.RowType, rowsObjects);

            var fileNameBase = $"{definition.FileNameBase}_{DateTime.UtcNow:yyyyMMdd_HHmm}";
            var fileName = $"{fileNameBase}{format.GetFileExtension()}";
            var contentType = format.GetContentType();
            var title = definition.Title;

            if (!Enum.IsDefined(format))
            {
                return BadRequest("Nieobsługiwany format eksportu.");
            }

            var fileBytes = format switch
            {
                ExportFormat.Csv => InvokeExport(nameof(IReportExportService.ExportCsv), definition.RowType, typedRows, fileNameBase),
                ExportFormat.Pdf => InvokeExport(nameof(IReportExportService.ExportPdf), definition.RowType, typedRows, title, fileNameBase),
                ExportFormat.Xlsx => InvokeExport(nameof(IReportExportService.ExportXlsx), definition.RowType, typedRows, title, fileNameBase),
                ExportFormat.Docx => InvokeExport(nameof(IReportExportService.ExportDocx), definition.RowType, typedRows, title, fileNameBase),
                _ => throw new ArgumentOutOfRangeException(nameof(format), format, "Nieobsługiwany format eksportu.")
            };

            var storageMode = _configuration.GetValue<ExportStorageMode>("ExportStorage:Mode");
            if (archive || storageMode == ExportStorageMode.Archive)
            {
                var now = DateTime.UtcNow;
                var relativePath = $"Exports/{now:yyyy}/{now:MM}/{now:dd}/raporty/{reportKey}/{fileName}";
                await _exportArchiveService.SaveAsync(fileBytes, relativePath, ct);
            }

            return File(fileBytes, contentType, fileName);
        }

        [HttpGet("podglad-danych")]
        public async Task<IActionResult> DataPreview(
            [FromQuery] string reportKey,
            [FromQuery] int take = 50,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(reportKey))
            {
                return BadRequest("Brak klucza raportu.");
            }

            if (take < 1)
            {
                take = 1;
            }
            else if (take > 200)
            {
                take = 200;
            }

            try
            {
                _ = _reportRegistry.GetByKey(reportKey);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            var response = await _reportDataPreviewService.BuildAsync(reportKey, take, ct);
            return Ok(response);
        }

        [HttpGet("podglad-dokumentu")]
        public async Task<IActionResult> DocumentPreview(
            [FromQuery] string reportKey,
            [FromQuery] int take = 50,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(reportKey))
            {
                return BadRequest("Brak klucza raportu.");
            }

            if (take < 1)
            {
                take = 1;
            }
            else if (take > 200)
            {
                take = 200;
            }

            try
            {
                _ = _reportRegistry.GetByKey(reportKey);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            var response = await _reportDocumentPreviewService.CreatePdfPreviewAsync(reportKey, take, ct);
            return Ok(response);
        }

        [HttpGet("podglad-dokumentu/{previewId}")]
        public IActionResult DocumentPreviewStream([FromRoute] string previewId)
        {
            if (string.IsNullOrWhiteSpace(previewId))
            {
                return NotFound();
            }

            var cacheKey = $"report-doc-preview:{previewId}";
            if (!_cache.TryGetValue(cacheKey, out (byte[] Bytes, string FileName, string ContentType) entry))
            {
                return NotFound();
            }

            Response.Headers["Content-Disposition"] = $"inline; filename=\"{entry.FileName}\"";
            Response.Headers["Cache-Control"] = "no-store";
            return File(entry.Bytes, entry.ContentType);
        }

        private byte[] InvokeExport(string methodName, Type rowType, params object[] parameters)
        {
            var method = typeof(IReportExportService)
                .GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance)
                ?.MakeGenericMethod(rowType);

            if (method is null)
            {
                throw new InvalidOperationException($"Brak metody eksportu: {methodName}.");
            }

            return (byte[])method.Invoke(_reportExportService, parameters)!;
        }

        private static object CreateTypedRows(Type rowType, IReadOnlyList<object> rows)
        {
            var listType = typeof(List<>).MakeGenericType(rowType);
            var list = (IList)Activator.CreateInstance(listType)!;

            foreach (var row in rows)
            {
                list.Add(row);
            }

            return list;
        }
    }
}