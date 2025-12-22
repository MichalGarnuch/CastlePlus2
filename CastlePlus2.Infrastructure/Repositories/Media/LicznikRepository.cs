// PLIK: CastlePlus2.Infrastructure/Repositories/Media/LicznikRepository.cs
// (CAŁY PLIK - bo dodajemy GetAll/GetForUpdate/Remove + overload NumerExists)

using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Licznik>> GetAllAsync(CancellationToken ct)
        {
            return await _db.Liczniki
                .AsNoTracking()
                .OrderBy(x => x.IdLicznika)
                .ToListAsync(ct);
        }

        public async Task<Licznik?> GetByIdAsync(long idLicznika, CancellationToken ct)
        {
            return await _db.Liczniki
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdLicznika == idLicznika, ct);
        }

        public async Task<Licznik?> GetForUpdateAsync(long idLicznika, CancellationToken ct)
        {
            return await _db.Liczniki
                .FirstOrDefaultAsync(x => x.IdLicznika == idLicznika, ct);
        }

        public async Task<bool> NumerExistsAsync(string numerNV, CancellationToken ct)
        {
            return await _db.Liczniki
                .AsNoTracking()
                .AnyAsync(x => x.NumerNV == numerNV, ct);
        }

        public async Task<bool> NumerExistsAsync(string numerNV, long excludeIdLicznika, CancellationToken ct)
        {
            return await _db.Liczniki
                .AsNoTracking()
                .AnyAsync(x => x.NumerNV == numerNV && x.IdLicznika != excludeIdLicznika, ct);
        }

        public async Task<bool> PrzylaczeExistsAsync(long idPrzylacza, CancellationToken ct)
        {
            return await _db.Przylacza
                .AsNoTracking()
                .AnyAsync(x => x.IdPrzylacza == idPrzylacza, ct);
        }

        public async Task<bool> JednostkaExistsAsync(string kodJednostki, CancellationToken ct)
        {
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

        public void Remove(Licznik entity)
        {
            _db.Liczniki.Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
