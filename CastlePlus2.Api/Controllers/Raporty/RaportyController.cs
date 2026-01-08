using CastlePlus2.Application.Interfaces.Exports;
using CastlePlus2.Application.Interfaces.Reports;
using CastlePlus2.Contracts.Exports;
using CastlePlus2.Infrastructure.Services.Reports.Definitions;
using Microsoft.AspNetCore.Mvc;
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

        public RaportyController(IReportExportService reportExportService, IReportRegistry reportRegistry)
        {
            _reportExportService = reportExportService;
            _reportRegistry = reportRegistry;
        }

        [HttpGet("eksport")]
        public async Task<IActionResult> Export(
            [FromQuery] string reportKey,
            [FromQuery] ExportFormat format,
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

            return File(fileBytes, contentType, fileName);
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