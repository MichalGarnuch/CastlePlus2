using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Application.Interfaces.Rdzen
{
    public interface IPrzypisanieAdresuRepository
    {
        Task<PrzypisanieAdresu?> GetByIdAsync(long id, CancellationToken cancellationToken);
        Task AddAsync(PrzypisanieAdresu entity, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Zwraca aktywne przypisanie adresu dla encji (DoDnia == null), jeżeli istnieje.
        /// </summary>
        Task<PrzypisanieAdresu?> GetAktywneDlaEncjiAsync(Guid idEncji, CancellationToken cancellationToken);
        Task<List<PrzypisanieAdresu>> GetAllAsync(CancellationToken cancellationToken);
        Task<PrzypisanieAdresu?> GetForUpdateAsync(long id, CancellationToken cancellationToken);
        void Remove(PrzypisanieAdresu entity);

    }
}
