using CastlePlus2.Application.Interfaces.Dokumenty;

namespace CastlePlus2.Infrastructure.Services.Exports
{
    public class DokumentFileStorage : IDokumentFileStorage
    {
        private readonly ExportStorageOptions _options;

        public DokumentFileStorage(ExportStorageOptions options)
        {
            _options = options;
        }

        public async Task<byte[]> ReadAllBytesAsync(string relativeOrAbsolutePath, CancellationToken ct)
        {
            var fullPath = ResolvePath(relativeOrAbsolutePath);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Nie znaleziono pliku: {fullPath}");

            return await File.ReadAllBytesAsync(fullPath, ct);
        }

        private string ResolvePath(string path)
        {
            if (Path.IsPathRooted(path))
                return path;

            if (string.IsNullOrWhiteSpace(_options.RootPath))
                throw new InvalidOperationException("Brak skonfigurowanego RootPath dla storage.");

            return Path.Combine(_options.RootPath, path.Replace('/', Path.DirectorySeparatorChar));
        }
    }
}