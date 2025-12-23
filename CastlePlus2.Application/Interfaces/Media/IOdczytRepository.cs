using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Media;

namespace CastlePlus2.Application.Interfaces.Media
{
    public interface IOdczytRepository
    {
        Task<Odczyt?> GetByIdAsync(long idOdczytu, CancellationToken ct);

        Task<List<Odczyt>> GetAllAsync(CancellationToken ct);
        Task<Odczyt?> GetForUpdateAsync(long idOdczytu, CancellationToken ct);

        Task<bool> LicznikExistsAsync(long idLicznika, CancellationToken ct);
        Task<bool> ExistsForLicznikAndDateAsync(long idLicznika, DateTime dataOdczytu, CancellationToken ct);

        Task AddAsync(Odczyt entity, CancellationToken ct);
        void Remove(Odczyt entity);

        Task SaveChangesAsync(CancellationToken ct);
    }
}
