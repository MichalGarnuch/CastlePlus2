using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace CastlePlus2.Infrastructure.Services.Exports;

public sealed class CsvReportExporter
{
    public byte[] Export<T>(IReadOnlyList<T> rows)
    {
        var properties = ExportCommon.GetExportableProperties<T>();

        var configuration = new CsvConfiguration(ExportCommon.ExportCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true
        };

        using var memory = new MemoryStream();
        using var writer = new StreamWriter(
            memory,
            new UTF8Encoding(encoderShouldEmitUTF8Identifier: true),
            leaveOpen: true);

        using var csv = new CsvWriter(writer, configuration);

        foreach (var property in properties)
            csv.WriteField(property.Name);

        csv.NextRecord();

        foreach (var row in rows)
        {
            foreach (var property in properties)
                csv.WriteField(ExportCommon.FormatValue(property.GetValue(row)));

            csv.NextRecord();
        }

        writer.Flush();
        return memory.ToArray();
    }
}
