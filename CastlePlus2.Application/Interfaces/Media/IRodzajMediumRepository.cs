using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Media;

namespace CastlePlus2.Application.Interfaces.Media
{
    public interface IRodzajMediumRepository
    {
        Task<List<RodzajMedium>> GetAllAsync(CancellationToken ct);
        Task<RodzajMedium?> GetByIdAsync(string kodRodzaju, CancellationToken ct);
        Task<bool> ExistsAsync(string kodRodzaju, CancellationToken ct);

        Task AddAsync(RodzajMedium entity, CancellationToken ct);
        void Remove(RodzajMedium entity);

        Task SaveChangesAsync(CancellationToken ct);
    }
}
