// PLIK: CastlePlus2.Application/Interfaces/Finanse/IPozycjaKosztuRepository.cs
using CastlePlus2.Domain.Entities.Finanse;

namespace CastlePlus2.Application.Interfaces.Finanse
{
    public interface IPozycjaKosztuRepository
    {
        Task<PozycjaKosztu?> GetByIdAsync(long id, CancellationToken ct);
        Task<PozycjaKosztu?> GetForUpdateAsync(long id, CancellationToken ct);
        Task<List<PozycjaKosztu>> GetAllAsync(CancellationToken ct);

        Task AddAsync(PozycjaKosztu entity, CancellationToken ct);
        Task RemoveAsync(PozycjaKosztu entity, CancellationToken ct);

        Task SaveChangesAsync(CancellationToken ct);
    }
}
