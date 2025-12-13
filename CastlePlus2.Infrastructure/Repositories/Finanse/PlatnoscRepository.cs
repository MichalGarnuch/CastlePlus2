using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Finanse
{
    public class PlatnoscRepository : IPlatnoscRepository
    {
        private readonly CastlePlus2DbContext _db;

        public PlatnoscRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<Platnosc?> GetByIdAsync(long id, CancellationToken ct)
        {
            return await _db.Platnosci
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdPlatnosci == id, ct);
        }

        public async Task AddAsync(Platnosc entity, CancellationToken ct)
        {
            await _db.Platnosci.AddAsync(entity, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
