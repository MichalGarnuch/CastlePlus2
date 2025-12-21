using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Rdzen
{
    public class PrzypisanieAdresuRepository : IPrzypisanieAdresuRepository
    {
        private readonly CastlePlus2DbContext _db;

        public PrzypisanieAdresuRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public Task<PrzypisanieAdresu?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return _db.PrzypisaniaAdresow
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdPrzypisaniaAdresu == id, cancellationToken);
        }

        public Task<PrzypisanieAdresu?> GetAktywneDlaEncjiAsync(Guid idEncji, CancellationToken cancellationToken)
        {
            return _db.PrzypisaniaAdresow
                .FirstOrDefaultAsync(x => x.IdEncji == idEncji && x.DoDnia == null, cancellationToken);
        }

        public Task AddAsync(PrzypisanieAdresu entity, CancellationToken cancellationToken)
        {
            return _db.PrzypisaniaAdresow.AddAsync(entity, cancellationToken).AsTask();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _db.SaveChangesAsync(cancellationToken);
        }
        public Task<List<PrzypisanieAdresu>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _db.PrzypisaniaAdresow
                .AsNoTracking()
                .OrderByDescending(x => x.IdPrzypisaniaAdresu)
                .ToListAsync(cancellationToken);
        }

        public Task<PrzypisanieAdresu?> GetForUpdateAsync(long id, CancellationToken cancellationToken)
        {
            return _db.PrzypisaniaAdresow
                .FirstOrDefaultAsync(x => x.IdPrzypisaniaAdresu == id, cancellationToken);
        }

        public void Remove(PrzypisanieAdresu entity)
        {
            _db.PrzypisaniaAdresow.Remove(entity);
        }

    }
}
