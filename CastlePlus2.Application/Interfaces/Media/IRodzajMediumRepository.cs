using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Media;

namespace CastlePlus2.Application.Interfaces.Media
{
    public interface IRodzajMediumRepository
    {
        Task<RodzajMedium?> GetByIdAsync(string kodRodzaju, CancellationToken ct);
        Task<bool> ExistsAsync(string kodRodzaju, CancellationToken ct);

        Task AddAsync(RodzajMedium entity, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
