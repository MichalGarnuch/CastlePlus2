using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Domain.Entities.Media;
using CastlePlus2.Domain.Entities.Rdzen;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Media
{
    public class PrzylaczeRepository : IPrzylaczeRepository
    {
        private readonly CastlePlus2DbContext _db;

        public PrzylaczeRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<Przylacze?> GetByIdAsync(long idPrzylacza, CancellationToken ct = default)
        {
            return await _db.Przylacza
                .AsNoTracking()
                .Include(x => x.RodzajMedium)
                .FirstOrDefaultAsync(x => x.IdPrzylacza == idPrzylacza, ct);
        }

        public async Task<List<Przylacze>> GetAllAsync(CancellationToken ct = default)
        {
            return await _db.Przylacza
                .AsNoTracking()
                .Include(x => x.RodzajMedium)
                .OrderBy(x => x.IdPrzylacza)
                .ToListAsync(ct);
        }

        public async Task<Przylacze?> GetForUpdateAsync(long idPrzylacza, CancellationToken ct = default)
        {
            return await _db.Przylacza
                .Include(x => x.RodzajMedium)
                .FirstOrDefaultAsync(x => x.IdPrzylacza == idPrzylacza, ct);
        }

        public async Task AddAsync(Przylacze entity, CancellationToken ct = default)
        {
            await _db.Przylacza.AddAsync(entity, ct);
        }

        public void Remove(Przylacze entity)
        {
            _db.Przylacza.Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _db.SaveChangesAsync(ct);
        }

        public async Task<bool> EncjaExistsAsync(Guid idEncji, CancellationToken ct = default)
        {
            return await _db.Set<Encja>()
                .AsNoTracking()
                .AnyAsync(e => e.Id == idEncji, ct);
        }
    }
}
