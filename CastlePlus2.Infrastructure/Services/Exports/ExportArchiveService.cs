using CastlePlus2.Application.Interfaces.Exports;

namespace CastlePlus2.Infrastructure.Services.Exports;

public sealed class ExportArchiveService : IExportArchiveService
{
    private readonly ExportStorageOptions _options;

    public ExportArchiveService(ExportStorageOptions options)
    {
        _options = options;
    }

    public async Task<string> SaveAsync(byte[] bytes, string relativePath, CancellationToken ct)
    {
        ValidateRelativePath(relativePath);

        var fullPath = Path.Combine(_options.RootPath, relativePath);
        var directory = Path.GetDirectoryName(fullPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 81920, useAsync: true);
        await stream.WriteAsync(bytes, ct);
        await stream.FlushAsync(ct);

        return relativePath;
    }

    public Task DeleteOlderThanAsync(int retentionDays, CancellationToken ct)
    {
        if (retentionDays <= 0 || string.IsNullOrWhiteSpace(_options.RootPath))
        {
            return Task.CompletedTask;
        }

        var cutoff = DateTime.UtcNow.AddDays(-retentionDays);
        if (!Directory.Exists(_options.RootPath))
        {
            return Task.CompletedTask;
        }

        var directories = new Stack<string>();
        directories.Push(_options.RootPath);

        while (directories.Count > 0)
        {
            ct.ThrowIfCancellationRequested();
            var current = directories.Pop();
            foreach (var directory in Directory.GetDirectories(current))
            {
                directories.Push(directory);
            }

            foreach (var file in Directory.GetFiles(current))
            {
                ct.ThrowIfCancellationRequested();
                var lastWriteUtc = File.GetLastWriteTimeUtc(file);
                if (lastWriteUtc < cutoff)
                {
                    File.Delete(file);
                }
            }
        }

        RemoveEmptyDirectories(_options.RootPath);
        return Task.CompletedTask;
    }

    private static void ValidateRelativePath(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
        {
            throw new ArgumentException("Relative path is required.", nameof(relativePath));
        }

        if (Path.IsPathRooted(relativePath))
        {
            throw new ArgumentException("Relative path cannot be rooted.", nameof(relativePath));
        }

        if (relativePath.Contains("..", StringComparison.Ordinal))
        {
            throw new ArgumentException("Relative path cannot contain parent directory segments.", nameof(relativePath));
        }

        if (relativePath.Contains(':', StringComparison.Ordinal))
        {
            throw new ArgumentException("Relative path cannot contain drive specifiers.", nameof(relativePath));
        }

        if (relativePath.StartsWith("/", StringComparison.Ordinal) || relativePath.StartsWith("\\", StringComparison.Ordinal))
        {
            throw new ArgumentException("Relative path cannot start with a directory separator.", nameof(relativePath));
        }
    }

    private static void RemoveEmptyDirectories(string rootPath)
    {
        var directories = Directory.GetDirectories(rootPath, "*", SearchOption.AllDirectories)
            .OrderByDescending(path => path.Length)
            .ToArray();

        foreach (var directory in directories)
        {
            if (!Directory.Exists(directory))
            {
                continue;
            }

            if (Directory.EnumerateFileSystemEntries(directory).Any())
            {
                continue;
            }

            Directory.Delete(directory, false);
        }
    }
}