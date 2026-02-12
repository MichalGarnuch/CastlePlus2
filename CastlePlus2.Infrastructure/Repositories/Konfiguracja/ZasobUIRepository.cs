using CastlePlus2.Application.Interfaces.Konfiguracja;
using CastlePlus2.Domain.Entities.Konfiguracja;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Konfiguracja;

public class ZasobUIRepository : IZasobUIRepository
{
    private readonly CastlePlus2DbContext _db;

    public ZasobUIRepository(CastlePlus2DbContext db)
    {
        _db = db;
    }

    public Task<ZasobUI?> GetByIdAsync(Guid idEncji, CancellationToken ct)
        => _db.ZasobyUI
            .AsNoTracking()
            .Include(x => x.Teksty)
            .FirstOrDefaultAsync(x => x.IdEncji == idEncji, ct);

    public Task<ZasobUI?> GetForUpdateAsync(Guid idEncji, CancellationToken ct)
        => _db.ZasobyUI
            .Include(x => x.Teksty)
            .FirstOrDefaultAsync(x => x.IdEncji == idEncji, ct);

    public Task<ZasobUI?> GetByKodZasobuAsync(string kodZasobu, CancellationToken ct)
        => _db.ZasobyUI
            .AsNoTracking()
            .Include(x => x.Teksty)
            .FirstOrDefaultAsync(x => x.KodZasobu == kodZasobu, ct);

    public Task<List<ZasobUI>> GetAllAsync(string? typ, string? kategoria, bool? czyAktywny, CancellationToken ct)
    {
        IQueryable<ZasobUI> q = _db.ZasobyUI.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(typ))
            q = q.Where(x => x.Typ == typ);

        if (!string.IsNullOrWhiteSpace(kategoria))
            q = q.Where(x => x.Kategoria == kategoria);

        if (czyAktywny.HasValue)
            q = q.Where(x => x.CzyAktywny == czyAktywny.Value);

        return q
            .OrderBy(x => x.Typ)
            .ThenBy(x => x.Kategoria)
            .ThenBy(x => x.Sort)
            .ThenBy(x => x.KodZasobu)
            .ToListAsync(ct);
    }

    public Task<List<ZasobUI>> GetPublicAsync(string typ, string? kategoria, bool includeInactive, DateTime nowUtc, CancellationToken ct)
    {
        IQueryable<ZasobUI> q = _db.ZasobyUI
            .AsNoTracking()
            .Include(x => x.Teksty)
            .Where(x => x.Typ == typ);

        if (!string.IsNullOrWhiteSpace(kategoria))
            q = q.Where(x => x.Kategoria == kategoria);

        // okno ważności zawsze respektujemy dla publicznych treści
        q = q.Where(x =>
            (x.WazneOdUtc == null || x.WazneOdUtc <= nowUtc) &&
            (x.WazneDoUtc == null || x.WazneDoUtc >= nowUtc));

        if (!includeInactive)
            q = q.Where(x => x.CzyAktywny);

        return q
            .OrderByDescending(x => x.Sort)
            .ThenBy(x => x.KodZasobu)
            .ToListAsync(ct);
    }

    public Task AddAsync(ZasobUI entity, CancellationToken ct)
        => _db.ZasobyUI.AddAsync(entity, ct).AsTask();

    public void Remove(ZasobUI entity)
        => _db.ZasobyUI.Remove(entity);

    public Task<int> SaveChangesAsync(CancellationToken ct)
        => _db.SaveChangesAsync(ct);
}
