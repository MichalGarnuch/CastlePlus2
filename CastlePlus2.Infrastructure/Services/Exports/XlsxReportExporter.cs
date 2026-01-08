using ClosedXML.Excel;

namespace CastlePlus2.Infrastructure.Services.Exports;

public sealed class XlsxReportExporter
{
    public byte[] Export<T>(IReadOnlyList<T> rows, string title)
    {
        var properties = ExportCommon.GetExportableProperties<T>();

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Raport");

        // Tytuł (opcjonalnie)
        worksheet.Cell(1, 1).Value = title;
        worksheet.Cell(1, 1).Style.Font.Bold = true;

        // Nagłówki od wiersza 3
        for (var col = 0; col < properties.Length; col++)
        {
            worksheet.Cell(3, col + 1).Value = properties[col].Name;
            worksheet.Cell(3, col + 1).Style.Font.Bold = true;
        }

        // Dane od wiersza 4
        for (var rowIndex = 0; rowIndex < rows.Count; rowIndex++)
        {
            for (var colIndex = 0; colIndex < properties.Length; colIndex++)
            {
                var value = properties[colIndex].GetValue(rows[rowIndex]);
                worksheet.Cell(rowIndex + 4, colIndex + 1).Value = ExportCommon.FormatValue(value);
            }
        }

        worksheet.SheetView.FreezeRows(3);
        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
