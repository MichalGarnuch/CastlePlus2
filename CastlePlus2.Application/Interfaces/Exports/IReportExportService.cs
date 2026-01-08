using System.Collections.Generic;

namespace CastlePlus2.Application.Interfaces.Exports;

public interface IReportExportService
{
    byte[] ExportCsv<T>(IReadOnlyList<T> rows, string fileNameBase);
    byte[] ExportPdf<T>(IReadOnlyList<T> rows, string title, string fileNameBase);
    byte[] ExportXlsx<T>(IReadOnlyList<T> rows, string title, string fileNameBase);
    byte[] ExportDocx<T>(IReadOnlyList<T> rows, string title, string fileNameBase);
}