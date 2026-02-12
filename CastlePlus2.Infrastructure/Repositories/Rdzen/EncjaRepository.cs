using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Rdzen
{
    public class EncjaRepository : IEncjaRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public EncjaRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Encja?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Encje
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<List<Encja>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Encje
                .AsNoTracking()
                .OrderByDescending(e => e.UtworzonoUtc)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Encja entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Encje.AddAsync(entity, cancellationToken);
        }

        public async Task<Encja?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Encje
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public void Remove(Encja entity)
        {
            _dbContext.Encje.Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<Encja>> SearchAsync(string? typEncji, string? q, int take, CancellationToken cancellationToken = default)
        {
            take = take <= 0 ? 50 : Math.Min(take, 200);

            var query = _dbContext.Encje.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(typEncji))
            {
                var t = typEncji.Trim();
                query = query.Where(e => e.TypEncji == t);
            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                var s = q.Trim();
                query = query.Where(e =>
                    e.TypEncji.Contains(s) ||
                    (e.KodEncji != null && e.KodEncji.Contains(s)));
            }

            return await query
                .OrderBy(e => e.TypEncji)
                .ThenBy(e => e.KodEncji)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task<(List<Encja> Items, int TotalCount)> SearchPagedAsync(
            string? typEncji,
            string? q,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var currentPage = page <= 0 ? 1 : page;
            var currentPageSize = pageSize <= 0 ? 20 : Math.Min(pageSize, 200);

            var query = _dbContext.Encje.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(typEncji))
            {
                var t = typEncji.Trim();
                query = query.Where(e => e.TypEncji == t);
            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                var s = q.Trim();
                query = query.Where(e =>
                    e.TypEncji.Contains(s) ||
                    (e.KodEncji != null && e.KodEncji.Contains(s)));
            }

            var total = await query.CountAsync(cancellationToken);
            var items = await query
                .OrderBy(e => e.TypEncji)
                .ThenBy(e => e.KodEncji)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToListAsync(cancellationToken);

            return (items, total);
        }
    }
}