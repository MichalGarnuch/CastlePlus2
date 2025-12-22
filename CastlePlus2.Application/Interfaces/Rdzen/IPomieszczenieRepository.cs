using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Application.Interfaces.Rdzen
{
    /// <summary>
    /// Repozytorium domenowe dla encji Pomieszczenie.
    /// </summary>
    public interface IPomieszczenieRepository
    {
        Task<Pomieszczenie?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Pomieszczenie>> GetByLokalAsync(
            Guid idEncjiLokal,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsWithCodeForLokalAsync(
            Guid idEncjiLokal,
            string kodPomieszczenia,
            CancellationToken cancellationToken = default);

        Task AddAsync(Pomieszczenie entity, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<List<Pomieszczenie>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Pomieszczenie?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken = default);
        void Remove(Pomieszczenie entity);

    }
}
