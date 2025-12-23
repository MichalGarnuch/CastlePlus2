using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Application.Interfaces.Rdzen
{
    /// <summary>
    /// Interfejs repozytorium dla encji Encja.
    /// </summary>
    public interface IEncjaRepository
    {
        Task<Encja?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Encja>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Encja entity, CancellationToken cancellationToken = default);
        Task<Encja?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken = default);
        void Remove(Encja entity);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}