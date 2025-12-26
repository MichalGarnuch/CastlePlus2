// PLIK: CastlePlus2.Application/Interfaces/Dokumenty/IDokumentRepository.cs
using CastlePlus2.Domain.Entities.Dokumenty;

namespace CastlePlus2.Application.Interfaces.Dokumenty
{
    public interface IDokumentRepository
    {
        Task<Dokument?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<Dokument?> GetForUpdateAsync(long id, CancellationToken cancellationToken = default);
        Task<List<Dokument>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddAsync(Dokument entity, CancellationToken cancellationToken = default);

        // Dokument nie ma RowVersion -> update klasyczny przez EF tracking
        Task RemoveAsync(Dokument entity, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
