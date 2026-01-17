using System.Threading;
using System.Threading.Tasks;

namespace CastlePlus2.Client.Services.Exports;

public interface IFileInteropService
{
    Task DownloadAsync(string fileName, string contentType, byte[] bytes, CancellationToken ct = default);
    Task<string> CreateObjectUrlAsync(string contentType, byte[] bytes, CancellationToken ct = default);
    Task RevokeObjectUrlAsync(string objectUrl, CancellationToken ct = default);
}
