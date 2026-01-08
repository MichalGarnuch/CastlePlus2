using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Reflection;

namespace CastlePlus2.Infrastructure.Services.Exports;

public sealed class DocxReportExporter
{
    public byte[] Export<T>(IReadOnlyList<T> rows, string title)
    {
        var properties = ExportCommon.GetExportableProperties<T>();

        using var stream = new MemoryStream();
        using (var document = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document, true))
        {
            var mainPart = document.AddMainDocumentPart();
            mainPart.Document = new Document();

            var body = new Body();

            // Tytuł
            body.Append(CreateTitleParagraph(title));

            // Tabela
            body.Append(CreateTable(properties, rows));

            // Sekcja (Word lubi mieć SectionProperties na końcu Body)
            body.Append(new SectionProperties());

            mainPart.Document.Append(body);
            mainPart.Document.Save();
        }

        return stream.ToArray();
    }

    private static Paragraph CreateTitleParagraph(string text)
    {
        var runProps = new RunProperties(
            new Bold(),
            new FontSize { Val = "28" } // 14pt
        );

        var run = new Run(runProps, new Text(text) { Space = SpaceProcessingModeValues.Preserve });

        var p = new Paragraph(run);
        p.ParagraphProperties = new ParagraphProperties(
            new SpacingBetweenLines { After = "240" } // odstęp po tytule
        );

        return p;
    }

    private static Table CreateTable(PropertyInfo[] properties, IReadOnlyList<object> rowsBoxed)
        => throw new NotSupportedException("Use generic overload.");

    private static Table CreateTable<T>(PropertyInfo[] properties, IReadOnlyList<T> rows)
    {
        var table = new Table();

        // TableProperties + width + borders (żeby było pewne, że renderer to pokaże)
        var borders = new TableBorders(
            new TopBorder { Val = BorderValues.Single, Size = 6 },
            new BottomBorder { Val = BorderValues.Single, Size = 6 },
            new LeftBorder { Val = BorderValues.Single, Size = 6 },
            new RightBorder { Val = BorderValues.Single, Size = 6 },
            new InsideHorizontalBorder { Val = BorderValues.Single, Size = 6 },
            new InsideVerticalBorder { Val = BorderValues.Single, Size = 6 }
        );

        var tableProps = new TableProperties(
            new TableWidth { Type = TableWidthUnitValues.Pct, Width = "5000" }, // 100%
            borders
        );

        table.AppendChild(tableProps);

        // Grid (często pomaga, gdy Word/preview “gubi” tabelę)
        var grid = new TableGrid();
        for (var i = 0; i < properties.Length; i++)
            grid.Append(new GridColumn());
        table.Append(grid);

        // Header
        var headerRow = new TableRow();
        foreach (var p in properties)
            headerRow.Append(CreateCell(p.Name, bold: true, isHeader: true));
        table.Append(headerRow);

        // Data
        foreach (var row in rows)
        {
            var dataRow = new TableRow();
            foreach (var p in properties)
            {
                var val = p.GetValue(row);
                dataRow.Append(CreateCell(ExportCommon.FormatValue(val)));
            }
            table.Append(dataRow);
        }

        return table;
    }

    private static TableCell CreateCell(string text, bool bold = false, bool isHeader = false)
    {
        var runProps = new RunProperties(
            new FontSize { Val = "22" } // 11pt
        );

        if (bold)
            runProps.InsertAt(new Bold(), 0);

        var run = new Run(runProps, new Text(text ?? string.Empty) { Space = SpaceProcessingModeValues.Preserve });

        var paragraph = new Paragraph(run);

        var cellProps = new TableCellProperties(
            new TableCellWidth { Type = TableWidthUnitValues.Auto }
        );

        if (isHeader)
        {
            cellProps.Append(new Shading
            {
                Val = ShadingPatternValues.Clear,
                Color = "auto",
                Fill = "D9D9D9"
            });
        }

        var cell = new TableCell(cellProps, paragraph);

        return cell;
    }
}
