namespace CastlePlus2.Application.Interfaces.Exports;

public interface IExportArchiveService
{
    Task<string> SaveAsync(byte[] bytes, string relativePath, CancellationToken ct);

    Task DeleteOlderThanAsync(int retentionDays, CancellationToken ct);
}