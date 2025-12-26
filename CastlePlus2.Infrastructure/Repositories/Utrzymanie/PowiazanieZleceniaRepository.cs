using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using CastlePlus2.Domain.Entities.Utrzymanie;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Utrzymanie
{
    public class PowiazanieZleceniaRepository : IPowiazanieZleceniaRepository
    {
        private readonly CastlePlus2DbContext _db;

        public PowiazanieZleceniaRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public Task<List<PowiazanieZlecenia>> GetAllAsync(CancellationToken ct)
        {
            return _db.PowiazaniaZlecenia
                .AsNoTracking()
                .OrderBy(x => x.IdPowiazania)
                .ToListAsync(ct);
        }

        public async Task<PowiazanieZlecenia?> GetByIdAsync(long idPowiazania, CancellationToken ct)
        {
            return await _db.PowiazaniaZlecenia
                .FirstOrDefaultAsync(x => x.IdPowiazania == idPowiazania, ct);
        }

        public async Task<bool> ExistsAsync(long idZlecenia, Guid idEncji, CancellationToken ct)
        {
            return await _db.PowiazaniaZlecenia
                .AsNoTracking()
                .AnyAsync(x => x.IdZlecenia == idZlecenia && x.IdEncji == idEncji, ct);
        }

        public async Task AddAsync(PowiazanieZlecenia entity, CancellationToken ct)
        {
            await _db.PowiazaniaZlecenia.AddAsync(entity, ct);
        }

        public void Remove(PowiazanieZlecenia entity)
        {
            _db.PowiazaniaZlecenia.Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}