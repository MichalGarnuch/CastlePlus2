using System;
using System.Linq;
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

        public async Task<(List<Przylacze> Items, int TotalCount)> SearchPagedAsync(
            string? q,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var currentPage = page <= 0 ? 1 : page;
            var currentPageSize = pageSize <= 0 ? 20 : Math.Min(pageSize, 200);

            IQueryable<Przylacze> query = _db.Przylacza
    .AsNoTracking()
    .Include(x => x.RodzajMedium);


            if (!string.IsNullOrWhiteSpace(q))
            {
                var s = q.Trim();
                query = query.Where(p =>
                    p.IdPrzylacza.ToString().Contains(s)
                    || p.KodRodzaju.Contains(s)
                    || (p.Opis != null && p.Opis.Contains(s)));
            }

            var total = await query.CountAsync(ct);
            var items = await query
                .OrderBy(p => p.IdPrzylacza)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToListAsync(ct);

            return (items, total);
        }
    }
}
