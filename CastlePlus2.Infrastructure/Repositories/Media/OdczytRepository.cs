using System;
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

        public async Task<bool> LicznikExistsAsync(long idLicznika, CancellationToken ct)
        {
            return await _db.Liczniki
                .AsNoTracking()
                .AnyAsync(l => l.IdLicznika == idLicznika, ct);
        }

        public async Task<bool> ExistsForLicznikAndDateAsync(long idLicznika, DateTime dataOdczytu, CancellationToken ct)
        {
            var d = dataOdczytu.Date;
            return await _db.Odczyty
                .AsNoTracking()
                .AnyAsync(o => o.IdLicznika == idLicznika && o.DataOdczytu == d, ct);
        }

        public async Task AddAsync(Odczyt entity, CancellationToken ct)
        {
            await _db.Odczyty.AddAsync(entity, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
