using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Application.Interfaces.Rdzen
{
    /// <summary>
    /// Interfejs repozytorium dla encji Budynek.
    /// Umożliwia abstrakcję EF Core w warstwie Application.
    /// </summary>
    public interface IBudynekRepository
    {
        /// <summary>
        /// Pobranie budynku po IdEncji.
        /// </summary>
        Task<Budynek?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Dodanie nowego budynku do kontekstu.
        /// </summary>
        Task AddAsync(Budynek entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Zapis zmian w bazie.
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<List<Budynek>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Budynek?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken = default);
        void Remove(Budynek entity);

    }
}

