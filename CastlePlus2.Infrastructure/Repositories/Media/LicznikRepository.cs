using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Domain.Entities.Media;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Media
{
    public class LicznikRepository : ILicznikRepository
    {
        private readonly CastlePlus2DbContext _db;

        public LicznikRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<Licznik?> GetByIdAsync(long idLicznika, CancellationToken ct)
        {
            return await _db.Liczniki
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdLicznika == idLicznika, ct);
        }

        public async Task<bool> NumerExistsAsync(string numerNV, CancellationToken ct)
        {
            return await _db.Liczniki
                .AsNoTracking()
                .AnyAsync(x => x.NumerNV == numerNV, ct);
        }

        public async Task<bool> PrzylaczeExistsAsync(long idPrzylacza, CancellationToken ct)
        {
            // Feature-slice: nie wstrzykujemy repo od Przylacza tutaj.
            // Sprawdzamy istnienie po tabeli (DbSet), żeby walidować FK.
            return await _db.Przylacza
                .AsNoTracking()
                .AnyAsync(x => x.IdPrzylacza == idPrzylacza, ct);
        }

        public async Task<bool> JednostkaExistsAsync(string kodJednostki, CancellationToken ct)
        {
            // Nie robimy osobnego repo słownika, tylko szybki check FK.
            // Uwaga: musi istnieć encja JednostkaMiary w Domain + DbSet w DbContext.
            return await _db.Set<CastlePlus2.Domain.Entities.Slowniki.JednostkaMiary>()
                .AsNoTracking()
                .AnyAsync(x => x.KodJednostki == kodJednostki, ct);
        }

        public async Task<bool> LicznikExistsAsync(long idLicznika, CancellationToken ct)
        {
            return await _db.Liczniki
                .AsNoTracking()
                .AnyAsync(x => x.IdLicznika == idLicznika, ct);
        }

        public async Task AddAsync(Licznik entity, CancellationToken ct)
        {
            await _db.Liczniki.AddAsync(entity, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
