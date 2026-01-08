namespace CastlePlus2.Contracts.Exports;

public static class ExportFormatExtensions
{
    public static string GetFileExtension(this ExportFormat format)
    {
        return format switch
        {
            ExportFormat.Csv => ".csv",
            ExportFormat.Xlsx => ".xlsx",
            ExportFormat.Pdf => ".pdf",
            ExportFormat.Docx => ".docx",
            _ => string.Empty
        };
    }

    public static string GetContentType(this ExportFormat format)
    {
        return format switch
        {
            ExportFormat.Csv => "text/csv",
            ExportFormat.Xlsx => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ExportFormat.Pdf => "application/pdf",
            ExportFormat.Docx => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            _ => "application/octet-stream"
        };
    }
}