using System;
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
    public class OdczytRepository : IOdczytRepository
    {
        private readonly CastlePlus2DbContext _db;

        public OdczytRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<Odczyt?> GetByIdAsync(long idOdczytu, CancellationToken ct)
        {
            return await _db.Odczyty
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdOdczytu == idOdczytu, ct);
        }

        public async Task<List<Odczyt>> GetAllAsync(CancellationToken ct)
        {
            return await _db.Odczyty
                .AsNoTracking()
                .OrderByDescending(x => x.IdOdczytu)
                .ToListAsync(ct);
        }

        public async Task<Odczyt?> GetForUpdateAsync(long idOdczytu, CancellationToken ct)
        {
            return await _db.Odczyty
                .FirstOrDefaultAsync(x => x.IdOdczytu == idOdczytu, ct);
        }

        public async Task<bool> LicznikExistsAsync(long idLicznika, CancellationToken ct)
        {
            return await _db.Liczniki
                .AsNoTracking()
                .AnyAsync(x => x.IdLicznika == idLicznika, ct);
        }

        public async Task<bool> ExistsForLicznikAndDateAsync(long idLicznika, DateTime dataOdczytu, CancellationToken ct)
        {
            return await _db.Odczyty
                .AsNoTracking()
                .AnyAsync(x => x.IdLicznika == idLicznika && x.DataOdczytu == dataOdczytu.Date, ct);
        }

        public async Task AddAsync(Odczyt entity, CancellationToken ct)
        {
            await _db.Odczyty.AddAsync(entity, ct);
        }

        public void Remove(Odczyt entity)
        {
            _db.Odczyty.Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
