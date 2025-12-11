using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Application.Interfaces.Rdzen
{
    /// <summary>
    /// Interfejs repozytorium dla encji Lokal.
    /// </summary>
    public interface ILokalRepository
    {
        Task<Lokal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Lokal>> GetByBudynekAsync(
            Guid idBudynku,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsWithCodeForBudynekAsync(
            Guid idBudynku,
            string kodLokalu,
            CancellationToken cancellationToken = default);

        Task AddAsync(Lokal entity, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
