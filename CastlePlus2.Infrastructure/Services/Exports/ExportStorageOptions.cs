namespace CastlePlus2.Infrastructure.Services.Exports;

public sealed class ExportStorageOptions
{
    public string RootPath { get; set; } = string.Empty;

    public int RetentionDays { get; set; } = 30;
}