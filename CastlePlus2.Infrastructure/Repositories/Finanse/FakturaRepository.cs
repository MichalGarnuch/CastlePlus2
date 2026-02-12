using System;
using System.Linq;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Finanse
{
    public class FakturaRepository : IFakturaRepository
    {
        private readonly CastlePlus2DbContext _db;

        public FakturaRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<List<Faktura>> GetAllAsync(CancellationToken ct)
        {
            return await _db.Faktury
                .AsNoTracking()
                .OrderByDescending(x => x.IdFaktury)
                .ToListAsync(ct);
        }

        public async Task<Faktura?> GetByIdAsync(long idFaktury, CancellationToken ct)
        {
            return await _db.Faktury
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdFaktury == idFaktury, ct);
        }

        public async Task<Faktura?> GetForUpdateAsync(long idFaktury, CancellationToken ct)
        {
            // tracking (do update/delete)
            return await _db.Faktury
                .FirstOrDefaultAsync(x => x.IdFaktury == idFaktury, ct);
        }

        public async Task<bool> ExistsByNumerAsync(string numerFaktury, CancellationToken ct)
        {
            return await _db.Faktury
                .AsNoTracking()
                .AnyAsync(x => x.NumerFaktury == numerFaktury, ct);
        }

        public async Task<bool> ExistsByNumerAsync(string numerFaktury, long excludeIdFaktury, CancellationToken ct)
        {
            return await _db.Faktury
                .AsNoTracking()
                .AnyAsync(x => x.NumerFaktury == numerFaktury && x.IdFaktury != excludeIdFaktury, ct);
        }

        public async Task AddAsync(Faktura entity, CancellationToken ct)
        {
            await _db.Faktury.AddAsync(entity, ct);
        }

        public void Remove(Faktura entity)
        {
            _db.Faktury.Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }

        public async Task<(List<Faktura> Items, int TotalCount)> SearchPagedAsync(
            string? q,
            long? idPodmiotu,
            int page,
            int pageSize,
            CancellationToken ct)
        {
            var currentPage = page <= 0 ? 1 : page;
            var currentPageSize = pageSize <= 0 ? 20 : Math.Min(pageSize, 200);

            var query = _db.Faktury.AsNoTracking();

            if (idPodmiotu.HasValue && idPodmiotu.Value > 0)
            {
                query = query.Where(f => f.IdPodmiotu == idPodmiotu.Value);
            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                var s = q.Trim();
                query = query.Where(f =>
                    f.NumerFaktury.Contains(s)
                    || f.IdFaktury.ToString().Contains(s));
            }

            var total = await query.CountAsync(ct);
            var items = await query
                .OrderByDescending(f => f.DataWystawienia)
                .ThenByDescending(f => f.IdFaktury)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToListAsync(ct);

            return (items, total);
        }
    }
}
