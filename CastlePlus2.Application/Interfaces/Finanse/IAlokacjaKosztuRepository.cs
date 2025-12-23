using System;
using CastlePlus2.Domain.Entities.Finanse;

namespace CastlePlus2.Application.Interfaces.Finanse
{
    public interface IAlokacjaKosztuRepository
    {
        Task<AlokacjaKosztu?> GetByIdAsync(long idAlokacji, CancellationToken ct);
        Task<AlokacjaKosztu?> GetForUpdateAsync(long idAlokacji, CancellationToken ct);

        Task<List<AlokacjaKosztu>> GetAllAsync(CancellationToken ct);

        Task<bool> ExistsAsync(long idPozycjiKosztu, Guid idEncji, CancellationToken ct);
        Task<bool> ExistsOtherAsync(long idPozycjiKosztu, Guid idEncji, long excludeIdAlokacji, CancellationToken ct);

        Task<bool> EncjaExistsAsync(Guid idEncji, CancellationToken ct);
        Task<bool> PozycjaKosztuExistsAsync(long idPozycjiKosztu, CancellationToken ct);

        Task AddAsync(AlokacjaKosztu entity, CancellationToken ct);
        Task RemoveAsync(AlokacjaKosztu entity, CancellationToken ct);

        Task SaveChangesAsync(CancellationToken ct);
    }
}
