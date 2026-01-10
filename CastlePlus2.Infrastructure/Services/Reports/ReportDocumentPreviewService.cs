using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Exports;
using CastlePlus2.Application.Interfaces.Reports;
using CastlePlus2.Contracts.Reports;
using CastlePlus2.Infrastructure.Services.Reports.Definitions;
using Microsoft.Extensions.Caching.Memory;

namespace CastlePlus2.Infrastructure.Services.Reports;

public sealed class ReportDocumentPreviewService : IReportDocumentPreviewService
{
    private const string CacheKeyPrefix = "report-doc-preview:";
    private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(10);

    private readonly IReportRegistry _reportRegistry;
    private readonly IReportExportService _reportExportService;
    private readonly IMemoryCache _cache;

    public ReportDocumentPreviewService(
        IReportRegistry reportRegistry,
        IReportExportService reportExportService,
        IMemoryCache cache)
    {
        _reportRegistry = reportRegistry;
        _reportExportService = reportExportService;
        _cache = cache;
    }

    public async Task<ReportDocumentPreviewCreateResponse> CreatePdfPreviewAsync(
        string reportKey,
        int take,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(reportKey))
        {
            throw new ArgumentException("Brak klucza raportu.", nameof(reportKey));
        }

        if (take < 1)
        {
            take = 1;
        }
        else if (take > 200)
        {
            take = 200;
        }

        var definition = _reportRegistry.GetByKey(reportKey);
        var rowsObjects = definition is FakturyReportDefinition fakturyDefinition
            ? await fakturyDefinition.BuildRowsAsync(take, ct)
            : await definition.BuildRowsAsync(ct);

        var typedRows = CreateTypedRows(definition.RowType, rowsObjects);
        var fileNameBase = $"{definition.FileNameBase}_{DateTime.UtcNow:yyyyMMdd_HHmm}";
        var pdfBytes = ExportPdf(definition.RowType, typedRows, definition.Title, fileNameBase);

        var previewId = Guid.NewGuid().ToString("N");
        var fileName = $"{fileNameBase}.pdf";
        var cacheKey = $"{CacheKeyPrefix}{previewId}";
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = CacheTtl
        };

        _cache.Set(cacheKey, (Bytes: pdfBytes, FileName: fileName, ContentType: "application/pdf"), options);

        return new ReportDocumentPreviewCreateResponse(
            previewId,
            $"/api/raporty/podglad-dokumentu/{previewId}",
            DateTime.UtcNow.Add(CacheTtl),
            "application/pdf",
            fileName);
    }

    private byte[] ExportPdf(Type rowType, object typedRows, string title, string fileNameBase)
    {
        var method = typeof(IReportExportService)
            .GetMethod(nameof(IReportExportService.ExportPdf), BindingFlags.Public | BindingFlags.Instance)
            ?.MakeGenericMethod(rowType);

        if (method is null)
        {
            throw new InvalidOperationException("Brak metody eksportu PDF.");
        }

        return (byte[])method.Invoke(_reportExportService, new[] { typedRows, title, fileNameBase })!;
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