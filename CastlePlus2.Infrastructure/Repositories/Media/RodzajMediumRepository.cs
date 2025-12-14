using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Domain.Entities.Media;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Media
{
    public class RodzajMediumRepository : IRodzajMediumRepository
    {
        private readonly CastlePlus2DbContext _db;

        public RodzajMediumRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<RodzajMedium?> GetByIdAsync(string kodRodzaju, CancellationToken ct)
        {
            return await _db.RodzajeMediow
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.KodRodzaju == kodRodzaju, ct);
        }

        public async Task<bool> ExistsAsync(string kodRodzaju, CancellationToken ct)
        {
            return await _db.RodzajeMediow
                .AsNoTracking()
                .AnyAsync(x => x.KodRodzaju == kodRodzaju, ct);
        }

        public async Task AddAsync(RodzajMedium entity, CancellationToken ct)
        {
            await _db.RodzajeMediow.AddAsync(entity, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
