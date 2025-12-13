using System;
using CastlePlus2.Domain.Entities.Finanse;

namespace CastlePlus2.Application.Interfaces.Finanse
{
    public interface IAlokacjaKosztuRepository
    {
        Task<AlokacjaKosztu?> GetByIdAsync(long idAlokacji, CancellationToken ct);
        Task<bool> ExistsAsync(long idPozycjiKosztu, Guid idEncji, CancellationToken ct);

        Task AddAsync(AlokacjaKosztu entity, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
