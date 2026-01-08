using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.Tables;
using MigraDocCore.Rendering;

namespace CastlePlus2.Infrastructure.Services.Exports;

public sealed class PdfReportExporter
{
    public byte[] Export<T>(IReadOnlyList<T> rows, string title)
    {
        var properties = ExportCommon.GetExportableProperties<T>();

        var document = new Document();
        var section = document.AddSection();
        section.PageSetup.TopMargin = Unit.FromCentimeter(1.5);
        section.PageSetup.BottomMargin = Unit.FromCentimeter(1.5);
        section.PageSetup.LeftMargin = Unit.FromCentimeter(1.5);
        section.PageSetup.RightMargin = Unit.FromCentimeter(1.5);

        var titleParagraph = section.AddParagraph(title);
        titleParagraph.Format.Font.Size = 14;
        titleParagraph.Format.Font.Bold = true;
        titleParagraph.Format.SpaceAfter = Unit.FromCentimeter(0.5);

        var table = section.AddTable();
        table.Borders.Width = 0.5;
        table.Rows.LeftIndent = 0;

        // prosta adaptacja szerokości kolumn
        var usableWidthCm = 21.0 - 3.0; // A4 ~21cm, marginesy 1.5+1.5
        var colWidthCm = Math.Max(2.2, usableWidthCm / Math.Max(1, properties.Length));

        foreach (var _ in properties)
        {
            var column = table.AddColumn(Unit.FromCentimeter(colWidthCm));
            column.Format.Alignment = ParagraphAlignment.Left;
        }

        var headerRow = table.AddRow();
        headerRow.Shading.Color = Colors.LightGray;
        headerRow.Format.Font.Bold = true;

        for (var i = 0; i < properties.Length; i++)
            headerRow.Cells[i].AddParagraph(properties[i].Name);

        foreach (var row in rows)
        {
            var dataRow = table.AddRow();
            for (var i = 0; i < properties.Length; i++)
                dataRow.Cells[i].AddParagraph(ExportCommon.FormatValue(properties[i].GetValue(row)));
        }

        var renderer = new PdfDocumentRenderer(unicode: true)
        {
            Document = document
        };

        renderer.RenderDocument();

        using var stream = new MemoryStream();
        renderer.PdfDocument.Save(stream, closeStream: false);
        return stream.ToArray();
    }
}
