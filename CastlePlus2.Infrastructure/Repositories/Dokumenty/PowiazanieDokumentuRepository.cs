using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Domain.Entities.Dokumenty;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Dokumenty;

public sealed class PowiazanieDokumentuRepository : IPowiazanieDokumentuRepository
{
    private readonly CastlePlus2DbContext _db;

    public PowiazanieDokumentuRepository(CastlePlus2DbContext db)
    {
        _db = db;
    }

    public Task<PowiazanieDokumentu?> GetByIdAsync(long idPowiazania, CancellationToken ct)
    {
        return _db.PowiazaniaDokumentow
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IdPowiazania == idPowiazania, ct);
    }

    public Task<PowiazanieDokumentu?> GetForUpdateAsync(long idPowiazania, CancellationToken ct)
    {
        // Trackowana encja do update
        return _db.PowiazaniaDokumentow
            .FirstOrDefaultAsync(x => x.IdPowiazania == idPowiazania, ct);
    }

    public Task<List<PowiazanieDokumentu>> GetAllAsync(CancellationToken ct)
    {
        return _db.PowiazaniaDokumentow
            .AsNoTracking()
            .OrderByDescending(x => x.IdPowiazania)
            .ToListAsync(ct);
    }

    public Task<List<PowiazanieDokumentu>> GetByEncjaIdAsync(Guid idEncji, CancellationToken ct)
    {
        return _db.PowiazaniaDokumentow
            .AsNoTracking()
            .Where(x => x.IdEncji == idEncji)
            .OrderByDescending(x => x.IdPowiazania)
            .ToListAsync(ct);
    }

    public async Task<List<Dokument>> GetDokumentyByEncjaIdAsync(Guid idEncji, CancellationToken ct)
    {
        var dokumentIds = await _db.PowiazaniaDokumentow
            .AsNoTracking()
            .Where(x => x.IdEncji == idEncji)
            .Select(x => x.IdDokumentu)
            .Distinct()
            .ToListAsync(ct);

        if (dokumentIds.Count == 0)
            return new List<Dokument>();

        return await _db.Dokumenty
            .AsNoTracking()
            .Where(d => dokumentIds.Contains(d.IdDokumentu))
            .OrderByDescending(d => d.IdDokumentu)
            .ToListAsync(ct);
    }

    public async Task<Dictionary<Guid, List<Dokument>>> GetDokumentyByEncjeIdsAsync(List<Guid> encjaIds, CancellationToken ct)
    {
        if (encjaIds is null || encjaIds.Count == 0)
            return new Dictionary<Guid, List<Dokument>>();

        var powiazania = await _db.PowiazaniaDokumentow
            .AsNoTracking()
            .Where(p => encjaIds.Contains(p.IdEncji))
            .ToListAsync(ct);

        if (powiazania.Count == 0)
            return encjaIds.Distinct().ToDictionary(x => x, _ => new List<Dokument>());

        var dokumentIds = powiazania
            .Select(p => p.IdDokumentu)
            .Distinct()
            .ToList();

        var dokumenty = await _db.Dokumenty
            .AsNoTracking()
            .Where(d => dokumentIds.Contains(d.IdDokumentu))
            .ToListAsync(ct);

        var docsById = dokumenty.ToDictionary(d => d.IdDokumentu, d => d);

        var result = new Dictionary<Guid, List<Dokument>>();
        foreach (var encjaId in encjaIds.Distinct())
        {
            var idsForEncja = powiazania
                .Where(p => p.IdEncji == encjaId)
                .Select(p => p.IdDokumentu)
                .Distinct()
                .ToList();

            var list = new List<Dokument>();
            foreach (var docId in idsForEncja)
            {
                if (docsById.TryGetValue(docId, out var doc))
                    list.Add(doc);
            }

            list = list
                .OrderByDescending(d => d.IdDokumentu)
                .ToList();

            result[encjaId] = list;
        }

        return result;
    }

    public Task<bool> ExistsAsync(long idDokumentu, Guid idEncji, CancellationToken ct)
    {
        return _db.PowiazaniaDokumentow
            .AsNoTracking()
            .AnyAsync(x => x.IdDokumentu == idDokumentu && x.IdEncji == idEncji, ct);
    }

    public Task<bool> ExistsOtherAsync(long idDokumentu, Guid idEncji, long idPowiazania, CancellationToken ct)
    {
        return _db.PowiazaniaDokumentow
            .AsNoTracking()
            .AnyAsync(x =>
                x.IdDokumentu == idDokumentu &&
                x.IdEncji == idEncji &&
                x.IdPowiazania != idPowiazania, ct);
    }

    public Task AddAsync(PowiazanieDokumentu entity, CancellationToken ct)
    {
        return _db.PowiazaniaDokumentow.AddAsync(entity, ct).AsTask();
    }

    public Task RemoveAsync(PowiazanieDokumentu entity, CancellationToken ct)
    {
        _db.PowiazaniaDokumentow.Remove(entity);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct)
    {
        return _db.SaveChangesAsync(ct);
    }
}
