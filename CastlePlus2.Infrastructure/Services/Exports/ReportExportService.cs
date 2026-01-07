using System.Globalization;
using System.Reflection;
using System.Text;
using System.Linq;
using CastlePlus2.Application.Interfaces.Exports;
using CsvHelper;
using CsvHelper.Configuration;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CastlePlus2.Infrastructure.Services.Exports;

public sealed class ReportExportService : IReportExportService
{
    private static readonly CultureInfo ExportCulture = new("pl-PL");

    static ReportExportService()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public byte[] ExportCsv<T>(IReadOnlyList<T> rows, string fileNameBase)
    {
        var configuration = new CsvConfiguration(ExportCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true
        };

        using var memory = new MemoryStream();
        using var writer = new StreamWriter(memory, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), leaveOpen: true);
        using var csv = new CsvWriter(writer, configuration);

        csv.WriteRecords(rows);
        writer.Flush();

        return memory.ToArray();
    }

    public byte[] ExportPdf<T>(IReadOnlyList<T> rows, string title, string fileNameBase)
    {
        var properties = typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(property => property.GetMethod is not null && property.GetMethod.IsPublic)
            .ToArray();

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(text => text.FontSize(10));

                page.Content().Column(column =>
                {
                    column.Item().Text(title).FontSize(16).SemiBold();
                    column.Item().PaddingTop(10).Element(content => BuildTable(content, properties, rows));
                });
            });
        });

        return document.GeneratePdf();
    }

    private static void BuildTable<T>(IContainer container, PropertyInfo[] properties, IReadOnlyList<T> rows)
    {
        if (properties.Length == 0)
        {
            container.Text("Brak danych do wyświetlenia.");
            return;
        }

        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                foreach (var _ in properties)
                {
                    columns.RelativeColumn();
                }
            });

            table.Header(header =>
            {
                foreach (var property in properties)
                {
                    header.Cell().Element(HeaderStyle).Text(property.Name);
                }
            });

            foreach (var row in rows)
            {
                foreach (var property in properties)
                {
                    var value = property.GetValue(row);
                    table.Cell().Element(BodyStyle).Text(value?.ToString() ?? string.Empty);
                }
            }
        });
    }

    private static IContainer HeaderStyle(IContainer container)
    {
        return container
            .DefaultTextStyle(text => text.SemiBold())
            .Background(Colors.Grey.Lighten3)
            .Padding(4)
            .BorderBottom(1)
            .BorderColor(Colors.Grey.Lighten1);
    }

    private static IContainer BodyStyle(IContainer container)
    {
        return container
            .Padding(4)
            .BorderBottom(1)
            .BorderColor(Colors.Grey.Lighten3);
    }
}