using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Domain.Entities.Podmioty;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Podmioty
{
    public class KontaktRepository : IKontaktRepository
    {
        private readonly CastlePlus2DbContext _db;

        public KontaktRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<Kontakt?> GetByIdAsync(long idKontaktu, CancellationToken ct)
        {
            return await _db.Kontakty
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdKontaktu == idKontaktu, ct);
        }
        public async Task<Kontakt?> GetByIdForUpdateAsync(long idKontaktu, CancellationToken ct)
        {
            return await _db.Kontakty
                .FirstOrDefaultAsync(x => x.IdKontaktu == idKontaktu, ct);
        }
        public async Task<List<Kontakt>> GetByPodmiotIdAsync(long idPodmiotu, CancellationToken ct)
        {
            return await _db.Kontakty
                .AsNoTracking()
                .Where(x => x.IdPodmiotu == idPodmiotu)
                .OrderByDescending(x => x.IdKontaktu)
                .ToListAsync(ct);
        }

        public async Task<bool> PodmiotExistsAsync(long idPodmiotu, CancellationToken ct)
        {
            return await _db.Podmioty
                .AsNoTracking()
                .AnyAsync(p => p.IdPodmiotu == idPodmiotu, ct);
        }

        public async Task AddAsync(Kontakt kontakt, CancellationToken ct)
        {
            await _db.Kontakty.AddAsync(kontakt, ct);
        }
        public void Remove(Kontakt kontakt)
        {
            _db.Kontakty.Remove(kontakt);
        }
        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
