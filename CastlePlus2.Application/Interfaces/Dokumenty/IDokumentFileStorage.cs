namespace CastlePlus2.Application.Interfaces.Dokumenty
{
    public interface IDokumentFileStorage
    {
        Task<byte[]> ReadAllBytesAsync(string relativeOrAbsolutePath, CancellationToken ct);
    }
}