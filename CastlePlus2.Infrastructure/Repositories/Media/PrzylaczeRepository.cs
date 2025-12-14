using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Domain.Entities.Media;
using CastlePlus2.Domain.Entities.Rdzen;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Media
{
    public class PrzylaczeRepository : IPrzylaczeRepository
    {
        private readonly CastlePlus2DbContext _db;

        public PrzylaczeRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<Przylacze?> GetByIdAsync(long idPrzylacza, CancellationToken ct)
        {
            return await _db.Przylacza
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdPrzylacza == idPrzylacza, ct);
        }

        public async Task AddAsync(Przylacze entity, CancellationToken ct)
        {
            await _db.Przylacza.AddAsync(entity, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }

        public async Task<bool> EncjaExistsAsync(Guid idEncji, CancellationToken ct)
        {
            // Encja ma PK = Id (mapowane w EF na kolumnę [IdEncji]).
            return await _db.Set<Encja>()
                .AsNoTracking()
                .AnyAsync(e => e.Id == idEncji, ct);
        }
    }
}
