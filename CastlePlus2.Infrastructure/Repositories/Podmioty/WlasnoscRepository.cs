using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Domain.Entities.Podmioty;
using CastlePlus2.Domain.Entities.Rdzen;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Podmioty
{
    public class WlasnoscRepository : IWlasnoscRepository
    {
        private readonly CastlePlus2DbContext _db;

        public WlasnoscRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<Wlasnosc?> GetByIdAsync(long idWlasnosci, CancellationToken ct)
        {
            return await _db.Wlasnosci
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdWlasnosci == idWlasnosci, ct);
        }

        public async Task<IReadOnlyList<Wlasnosc>> GetByEncjaIdAsync(Guid idEncji, CancellationToken ct)
        {
            return await _db.Wlasnosci
                .AsNoTracking()
                .Where(x => x.IdEncji == idEncji)
                .OrderByDescending(x => x.OdDnia)
                .ToListAsync(ct);
        }

        public async Task AddAsync(Wlasnosc entity, CancellationToken ct)
        {
            await _db.Wlasnosci.AddAsync(entity, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }

        public async Task<bool> EncjaExistsAsync(Guid idEncji, CancellationToken ct)
        {
            // UWAGA: Encja ma w kodzie zwykle właściwość Id (mapowaną na kolumnę IdEncji).
            // Nie używamy IdEncji w C#, bo to nazwa kolumny, niekoniecznie nazwa property.
            return await _db.Set<Encja>()
                .AsNoTracking()
                .AnyAsync(e => e.Id == idEncji, ct);
        }

        public async Task<bool> PodmiotExistsAsync(long idPodmiotu, CancellationToken ct)
        {
            // Podmiot jest w schemacie [podmioty], PK = IdPodmiotu (bigint).
            // Zakładamy, że encja domenowa Podmiot jest już w projekcie i zmapowana w DbContext.
            return await _db.Set<CastlePlus2.Domain.Entities.Podmioty.Podmiot>()
                .AsNoTracking()
                .AnyAsync(p => p.IdPodmiotu == idPodmiotu, ct);
        }
    }
}
