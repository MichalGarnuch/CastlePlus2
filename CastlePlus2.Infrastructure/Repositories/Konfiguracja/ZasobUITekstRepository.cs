using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Konfiguracja;
using CastlePlus2.Domain.Entities.Konfiguracja;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Konfiguracja
{
    public class ZasobUITekstRepository : IZasobUITekstRepository
    {
        private readonly CastlePlus2DbContext _db;

        public ZasobUITekstRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public Task<ZasobUITekst?> GetByIdAsync(long idZasobuTekstu, CancellationToken ct)
            => _db.ZasobyUITeksty.AsNoTracking().FirstOrDefaultAsync(x => x.IdZasobuTekstu == idZasobuTekstu, ct);

        public Task<ZasobUITekst?> GetForUpdateAsync(long idZasobuTekstu, CancellationToken ct)
            => _db.ZasobyUITeksty.FirstOrDefaultAsync(x => x.IdZasobuTekstu == idZasobuTekstu, ct);

        public Task<ZasobUITekst?> GetByKeyAsync(Guid idEncji, string jezyk, string pole, CancellationToken ct)
            => _db.ZasobyUITeksty.AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdEncji == idEncji && x.Jezyk == jezyk && x.Pole == pole, ct);

        public Task<List<ZasobUITekst>> GetByEncjaIdAsync(Guid idEncji, CancellationToken ct)
            => _db.ZasobyUITeksty.AsNoTracking()
                .Where(x => x.IdEncji == idEncji)
                .OrderBy(x => x.Jezyk)
                .ThenBy(x => x.Pole)
                .ToListAsync(ct);

        public Task AddAsync(ZasobUITekst entity, CancellationToken ct)
            => _db.ZasobyUITeksty.AddAsync(entity, ct).AsTask();

        public void Remove(ZasobUITekst entity)
            => _db.ZasobyUITeksty.Remove(entity);

        public Task<int> SaveChangesAsync(CancellationToken ct)
            => _db.SaveChangesAsync(ct);
    }
}
