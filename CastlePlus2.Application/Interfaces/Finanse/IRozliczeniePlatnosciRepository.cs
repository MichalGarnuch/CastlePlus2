// PLIK: CastlePlus2.Application/Interfaces/Finanse/IRozliczeniePlatnosciRepository.cs
using CastlePlus2.Domain.Entities.Finanse;

namespace CastlePlus2.Application.Interfaces.Finanse
{
    public interface IRozliczeniePlatnosciRepository
    {
        Task<RozliczeniePlatnosci?> GetByIdAsync(long id, CancellationToken ct);
        Task<RozliczeniePlatnosci?> GetForUpdateAsync(long id, CancellationToken ct);
        Task<List<RozliczeniePlatnosci>> GetAllAsync(CancellationToken ct);

        Task AddAsync(RozliczeniePlatnosci entity, CancellationToken ct);
        Task RemoveAsync(RozliczeniePlatnosci entity, CancellationToken ct);

        Task SaveChangesAsync(CancellationToken ct);
    }
}
