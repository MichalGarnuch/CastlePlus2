using System;
using System.Linq;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Domain.Entities.Najem;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Najem
{
    public class PrzedmiotNajmuRepository : IPrzedmiotNajmuRepository
    {
        private readonly CastlePlus2DbContext _db;

        public PrzedmiotNajmuRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(PrzedmiotNajmu entity, CancellationToken ct)
        {
            await _db.PrzedmiotyNajmu.AddAsync(entity, ct);
        }

        public Task<PrzedmiotNajmu?> GetByIdAsync(long id, CancellationToken ct)
        {
            return _db.PrzedmiotyNajmu
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdPrzedmiotuNajmu == id, ct);
        }

        public Task<List<PrzedmiotNajmu>> GetAllAsync(CancellationToken ct)
        {
            return _db.PrzedmiotyNajmu
                .AsNoTracking()
                .OrderByDescending(x => x.IdPrzedmiotuNajmu)
                .ToListAsync(ct);
        }

        public Task<PrzedmiotNajmu?> GetForUpdateAsync(long id, CancellationToken ct)
        {
            return _db.PrzedmiotyNajmu.FirstOrDefaultAsync(x => x.IdPrzedmiotuNajmu == id, ct);
        }

        public Task<List<PrzedmiotNajmu>> GetOpenForUpdateByUmowaIdAsync(Guid idUmowyNajmu, DateOnly dataZakonczenia, CancellationToken ct)
        {
            return _db.PrzedmiotyNajmu
                .Where(x => x.IdUmowyNajmu == idUmowyNajmu
                            && (x.DoDnia == null || x.DoDnia > dataZakonczenia))
                .ToListAsync(ct);
        }
        public Task<bool> ExistsOverlapAsync(Guid idEncji, DateOnly odDnia, DateOnly? doDnia, CancellationToken ct)
        {
            var koniec = doDnia ?? DateOnly.MaxValue;

            return _db.PrzedmiotyNajmu.AnyAsync(x =>
                x.IdEncji == idEncji
                && x.OdDnia <= koniec
                && (x.DoDnia == null || x.DoDnia >= odDnia),
                ct);
        }
        public void Remove(PrzedmiotNajmu entity)
        {
            _db.PrzedmiotyNajmu.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }

        public async Task<(List<PrzedmiotNajmu> Items, int TotalCount)> SearchPagedAsync(
            string? q,
            Guid? idUmowyNajmu,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var currentPage = page <= 0 ? 1 : page;
            var currentPageSize = pageSize <= 0 ? 20 : Math.Min(pageSize, 200);

            var query = _db.PrzedmiotyNajmu.AsNoTracking();

            if (idUmowyNajmu.HasValue && idUmowyNajmu.Value != Guid.Empty)
            {
                query = query.Where(p => p.IdUmowyNajmu == idUmowyNajmu.Value);
            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                var s = q.Trim();
                query = query.Where(p =>
                    p.IdPrzedmiotuNajmu.ToString().Contains(s)
                    || p.IdUmowyNajmu.ToString().Contains(s)
                    || p.IdEncji.ToString().Contains(s));
            }

            var total = await query.CountAsync(ct);
            var items = await query
                .OrderByDescending(p => p.IdPrzedmiotuNajmu)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToListAsync(ct);

            return (items, total);
        }
    }
}
