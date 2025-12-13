using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Domain.Entities.Dokumenty;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Dokumenty
{
    public class PowiazanieDokumentuRepository : IPowiazanieDokumentuRepository
    {
        private readonly CastlePlus2DbContext _db;

        public PowiazanieDokumentuRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<PowiazanieDokumentu?> GetByIdAsync(long idPowiazania, CancellationToken ct)
        {
            return await _db.PowiazaniaDokumentow
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdPowiazania == idPowiazania, ct);
        }

        public async Task<bool> ExistsAsync(long idDokumentu, Guid idEncji, CancellationToken ct)
        {
            return await _db.PowiazaniaDokumentow
                .AsNoTracking()
                .AnyAsync(x => x.IdDokumentu == idDokumentu && x.IdEncji == idEncji, ct);
        }

        public async Task AddAsync(PowiazanieDokumentu entity, CancellationToken ct)
        {
            await _db.PowiazaniaDokumentow.AddAsync(entity, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
