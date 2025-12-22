using CastlePlus2.Domain.Entities.Media;

namespace CastlePlus2.Application.Interfaces.Media
{
    public interface IPrzylaczeRepository
    {
        Task<List<Przylacze>> GetAllAsync(CancellationToken ct = default);
        Task<Przylacze?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<Przylacze?> GetForUpdateAsync(long id, CancellationToken ct = default);

        Task<bool> EncjaExistsAsync(Guid idEncjiGospodarza, CancellationToken ct = default);

        Task AddAsync(Przylacze entity, CancellationToken ct = default);
        void Remove(Przylacze entity);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
