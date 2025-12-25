// PLIK: CastlePlus2.Infrastructure/Repositories/Finanse/RozliczeniePlatnosciRepository.cs
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

        public async Task<RozliczeniePlatnosci?> GetForUpdateAsync(long id, CancellationToken ct)
        {
            return await _db.RozliczeniaPlatnosci
                .FirstOrDefaultAsync(x => x.IdRozliczenia == id, ct);
        }

        public async Task<List<RozliczeniePlatnosci>> GetAllAsync(CancellationToken ct)
        {
            return await _db.RozliczeniaPlatnosci
                .AsNoTracking()
                .OrderByDescending(x => x.IdRozliczenia)
                .ToListAsync(ct);
        }

        public async Task AddAsync(RozliczeniePlatnosci entity, CancellationToken ct)
        {
            await _db.RozliczeniaPlatnosci.AddAsync(entity, ct);
        }

        public Task RemoveAsync(RozliczeniePlatnosci entity, CancellationToken ct)
        {
            _db.RozliczeniaPlatnosci.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
