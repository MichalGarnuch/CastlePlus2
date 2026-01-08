using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CastlePlus2.Infrastructure.Services.Exports;

public sealed class DocxReportExporter
{
    public byte[] Export<T>(IReadOnlyList<T> rows, string title)
    {
        var properties = ExportCommon.GetExportableProperties<T>();

        using var stream = new MemoryStream();
        using var document = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document, true);

        var mainPart = document.AddMainDocumentPart();
        mainPart.Document = new Document(new Body());

        var body = mainPart.Document.Body!;
        body.Append(CreateParagraph(title, bold: true, fontSize: "28"));

        var table = new Table();

        var tableProperties = new TableProperties(
            new TableBorders(
                new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 }
            )
        );

        table.AppendChild(tableProperties);

        var headerRow = new TableRow();
        foreach (var p in properties)
            headerRow.Append(CreateCell(p.Name, bold: true));

        table.Append(headerRow);

        foreach (var row in rows)
        {
            var dataRow = new TableRow();

            foreach (var p in properties)
                dataRow.Append(CreateCell(ExportCommon.FormatValue(p.GetValue(row))));

            table.Append(dataRow);
        }

        body.Append(table);
        mainPart.Document.Save();

        return stream.ToArray();
    }

    private static Paragraph CreateParagraph(string text, bool bold, string fontSize)
    {
        var runProperties = new RunProperties();

        if (bold)
            runProperties.AppendChild(new Bold());

        runProperties.AppendChild(new FontSize { Val = fontSize });

        var run = new Run(runProperties, new Text(text));
        return new Paragraph(run);
    }

    private static TableCell CreateCell(string text, bool bold = false)
    {
        var paragraph = CreateParagraph(text, bold, "22");
        return new TableCell(paragraph);
    }
}
