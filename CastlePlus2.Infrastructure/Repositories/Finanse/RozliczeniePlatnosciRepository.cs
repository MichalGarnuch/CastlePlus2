using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Finanse
{
    public class RozliczeniePlatnosciRepository : IRozliczeniePlatnosciRepository
    {
        private readonly CastlePlus2DbContext _db;

        public RozliczeniePlatnosciRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<RozliczeniePlatnosci?> GetByIdAsync(long id, CancellationToken ct)
        {
            return await _db.RozliczeniaPlatnosci
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdRozliczenia == id, ct);
        }

        public async Task AddAsync(RozliczeniePlatnosci entity, CancellationToken ct)
        {
            await _db.RozliczeniaPlatnosci.AddAsync(entity, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
