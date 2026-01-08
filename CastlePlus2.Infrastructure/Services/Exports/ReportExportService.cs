using System.Globalization;
using System.Reflection;
using CastlePlus2.Application.Interfaces.Exports;

namespace CastlePlus2.Infrastructure.Services.Exports;

public sealed class ReportExportService : IReportExportService
{
    private readonly CsvReportExporter _csv;
    private readonly XlsxReportExporter _xlsx;
    private readonly PdfReportExporter _pdf;
    private readonly DocxReportExporter _docx;

    public ReportExportService(
        CsvReportExporter csv,
        XlsxReportExporter xlsx,
        PdfReportExporter pdf,
        DocxReportExporter docx)
    {
        _csv = csv;
        _xlsx = xlsx;
        _pdf = pdf;
        _docx = docx;
    }

    public byte[] ExportCsv<T>(IReadOnlyList<T> rows, string fileNameBase)
        => _csv.Export(rows);

    public byte[] ExportXlsx<T>(IReadOnlyList<T> rows, string title, string fileNameBase)
        => _xlsx.Export(rows, title);

    public byte[] ExportPdf<T>(IReadOnlyList<T> rows, string title, string fileNameBase)
        => _pdf.Export(rows, title);

    public byte[] ExportDocx<T>(IReadOnlyList<T> rows, string title, string fileNameBase)
        => _docx.Export(rows, title);
}

internal static class ExportCommon
{
    internal static readonly CultureInfo ExportCulture = new("pl-PL");
    internal const string DateFormat = "yyyy-MM-dd";
    internal const string DateTimeFormat = "yyyy-MM-dd HH:mm";
    internal const string TimeFormat = "HH:mm";

    internal static PropertyInfo[] GetExportableProperties<T>()
    {
        return typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetMethod is not null && p.GetMethod.IsPublic)
            .Where(p => IsSimpleType(p.PropertyType))
            .OrderBy(p => p.Name)
            .ToArray();
    }

    internal static bool IsSimpleType(Type type)
    {
        var actualType = Nullable.GetUnderlyingType(type) ?? type;

        if (actualType.IsEnum) return true;

        if (actualType == typeof(string)
            || actualType == typeof(Guid)
            || actualType == typeof(bool)
            || actualType == typeof(DateTime)
            || actualType == typeof(DateOnly)
            || actualType == typeof(TimeOnly))
        {
            return true;
        }

        return actualType == typeof(byte)
            || actualType == typeof(sbyte)
            || actualType == typeof(short)
            || actualType == typeof(ushort)
            || actualType == typeof(int)
            || actualType == typeof(uint)
            || actualType == typeof(long)
            || actualType == typeof(ulong)
            || actualType == typeof(float)
            || actualType == typeof(double)
            || actualType == typeof(decimal);
    }

    internal static string FormatValue(object? value)
    {
        if (value is null) return string.Empty;

        return value switch
        {
            DateTime dt => dt.TimeOfDay == TimeSpan.Zero
                ? dt.ToString(DateFormat, ExportCulture)
                : dt.ToString(DateTimeFormat, ExportCulture),

            DateOnly d => d.ToString(DateFormat, ExportCulture),
            TimeOnly t => t.ToString(TimeFormat, ExportCulture),

            IFormattable f => f.ToString(null, ExportCulture),

            _ => value.ToString() ?? string.Empty
        };
    }
}
