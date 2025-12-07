using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Application.Interfaces.Rdzen
{
    /// <summary>
    /// Interfejs repozytorium dla encji Adres.
    /// </summary>
    public interface IAdresRepository
    {
        Task<Adres?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task AddAsync(Adres entity, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

