using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Domain.Entities.Media;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Media
{
    public sealed class RodzajMediumRepository : IRodzajMediumRepository
    {
        private readonly CastlePlus2DbContext _db;

        public RodzajMediumRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public Task<List<RodzajMedium>> GetAllAsync(CancellationToken ct)
        {
            return _db.RodzajeMediow
                .AsNoTracking()
                .OrderBy(x => x.KodRodzaju)
                .ToListAsync(ct);
        }

        public Task<RodzajMedium?> GetByIdAsync(string kodRodzaju, CancellationToken ct)
        {
            return _db.RodzajeMediow
                .FirstOrDefaultAsync(x => x.KodRodzaju == kodRodzaju, ct);
        }

        public Task<bool> ExistsAsync(string kodRodzaju, CancellationToken ct)
        {
            return _db.RodzajeMediow.AnyAsync(x => x.KodRodzaju == kodRodzaju, ct);
        }

        public Task AddAsync(RodzajMedium entity, CancellationToken ct)
        {
            return _db.RodzajeMediow.AddAsync(entity, ct).AsTask();
        }

        public void Remove(RodzajMedium entity)
        {
            _db.RodzajeMediow.Remove(entity);
        }

        public Task SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
