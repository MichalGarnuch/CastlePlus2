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

        public void Remove(PrzedmiotNajmu entity)
        {
            _db.PrzedmiotyNajmu.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
