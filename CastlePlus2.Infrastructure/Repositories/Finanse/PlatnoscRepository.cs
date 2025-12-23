using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Finanse
{
    public class PlatnoscRepository : IPlatnoscRepository
    {
        private readonly CastlePlus2DbContext _db;

        public PlatnoscRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<List<Platnosc>> GetAllAsync(CancellationToken ct)
        {
            return await _db.Platnosci
                .AsNoTracking()
                .OrderByDescending(x => x.IdPlatnosci)
                .ToListAsync(ct);
        }

        public async Task<Platnosc?> GetByIdAsync(long id, CancellationToken ct)
        {
            return await _db.Platnosci
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdPlatnosci == id, ct);
        }

        public async Task<Platnosc?> GetForUpdateAsync(long id, CancellationToken ct)
        {
            return await _db.Platnosci
                .FirstOrDefaultAsync(x => x.IdPlatnosci == id, ct);
        }

        public async Task<bool> PodmiotExistsAsync(long idPodmiotu, CancellationToken ct)
        {
            return await _db.Podmioty
                .AsNoTracking()
                .AnyAsync(x => x.IdPodmiotu == idPodmiotu, ct);
        }

        public async Task<bool> WalutaExistsAsync(string kodWaluty, CancellationToken ct)
        {
            var kod = (kodWaluty ?? string.Empty).Trim();
            if (kod.Length == 0) return false;

            return await _db.Waluty
                .AsNoTracking()
                .AnyAsync(x => x.KodWaluty == kod, ct);
        }

        public async Task AddAsync(Platnosc entity, CancellationToken ct)
        {
            await _db.Platnosci.AddAsync(entity, ct);
        }

        public void Remove(Platnosc entity)
        {
            _db.Platnosci.Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
