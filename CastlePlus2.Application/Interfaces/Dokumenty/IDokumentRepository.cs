using CastlePlus2.Domain.Entities.Dokumenty;

namespace CastlePlus2.Application.Interfaces.Dokumenty
{
    /// <summary>
    /// Interfejs repozytorium dla tabeli [dokumenty].[Dokument].
    /// </summary>
    public interface IDokumentRepository
    {
        Task<Dokument?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task AddAsync(Dokument entity, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
