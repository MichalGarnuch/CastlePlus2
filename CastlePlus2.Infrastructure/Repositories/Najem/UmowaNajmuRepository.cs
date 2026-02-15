using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Domain.Entities.Najem;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Najem
{
    public class UmowaNajmuRepository : IUmowaNajmuRepository
    {
        private readonly CastlePlus2DbContext _db;

        public UmowaNajmuRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(UmowaNajmu entity, CancellationToken ct)
        {
            await _db.UmowyNajmu.AddAsync(entity, ct);
        }

        public Task<UmowaNajmu?> GetByIdAsync(Guid idEncji, CancellationToken ct)
        {
            return _db.UmowyNajmu
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == idEncji, ct);
        }

        public Task<List<UmowaNajmu>> GetAllAsync(CancellationToken ct)
        {
            return _db.UmowyNajmu
                .AsNoTracking()
                .OrderByDescending(x => x.UtworzonoUtc)
                .ToListAsync(ct);
        }

        public Task<UmowaNajmu?> GetForUpdateAsync(Guid idEncji, CancellationToken ct)
        {
            return _db.UmowyNajmu
                .FirstOrDefaultAsync(x => x.Id == idEncji, ct);
        }

        public Task<List<UmowaNajmu>> GetActiveInRangeAsync(DateTime from, DateTime to, CancellationToken ct)
        {
            return _db.UmowyNajmu
                .AsNoTracking()
                .Where(x => x.DataPoczatku <= to && (x.DataZakonczenia == null || x.DataZakonczenia >= from))
                .OrderBy(x => x.DataPoczatku)
                .ToListAsync(ct);
        }

        public void Remove(UmowaNajmu entity)
        {
            _db.UmowyNajmu.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }

        public async Task<(List<UmowaNajmu> Items, int TotalCount)> SearchPagedAsync(
            string? q,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var currentPage = page <= 0 ? 1 : page;
            var currentPageSize = pageSize <= 0 ? 20 : Math.Min(pageSize, 200);

            IQueryable<UmowaNajmu> query = _db.UmowyNajmu.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(q))
            {
                var s = q.Trim();

                query = query.Where(u =>
                    (u.KodEncji != null && u.KodEncji.Contains(s)) ||
                    u.Id.ToString().Contains(s));
            }

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderByDescending(u => u.DataPoczatku)
                .ThenByDescending(u => u.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToListAsync(ct);

            return (items, total);
        }
    }
}
