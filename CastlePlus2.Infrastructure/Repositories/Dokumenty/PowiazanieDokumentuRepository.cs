// PLIK: CastlePlus2.Infrastructure/Repositories/Dokumenty/PowiazanieDokumentuRepository.cs
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

        public async Task<PowiazanieDokumentu?> GetForUpdateAsync(long idPowiazania, CancellationToken ct)
        {
            return await _db.PowiazaniaDokumentow
                .FirstOrDefaultAsync(x => x.IdPowiazania == idPowiazania, ct);
        }

        public async Task<List<PowiazanieDokumentu>> GetAllAsync(CancellationToken ct)
        {
            return await _db.PowiazaniaDokumentow
                .AsNoTracking()
                .OrderByDescending(x => x.IdPowiazania)
                .ToListAsync(ct);
        }

        public async Task<bool> ExistsAsync(long idDokumentu, Guid idEncji, CancellationToken ct)
        {
            return await _db.PowiazaniaDokumentow
                .AsNoTracking()
                .AnyAsync(x => x.IdDokumentu == idDokumentu && x.IdEncji == idEncji, ct);
        }

        public async Task<bool> ExistsOtherAsync(long idDokumentu, Guid idEncji, long excludeIdPowiazania, CancellationToken ct)
        {
            return await _db.PowiazaniaDokumentow
                .AsNoTracking()
                .AnyAsync(x => x.IdDokumentu == idDokumentu
                              && x.IdEncji == idEncji
                              && x.IdPowiazania != excludeIdPowiazania, ct);
        }

        public async Task AddAsync(PowiazanieDokumentu entity, CancellationToken ct)
        {
            await _db.PowiazaniaDokumentow.AddAsync(entity, ct);
        }

        public Task RemoveAsync(PowiazanieDokumentu entity, CancellationToken ct)
        {
            _db.PowiazaniaDokumentow.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
