using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Domain.Entities.Slowniki;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Slowniki
{
    public class IndeksacjaRepository : IIndeksacjaRepository
    {
        private readonly CastlePlus2DbContext _db;

        public IndeksacjaRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Indeksacja entity, CancellationToken ct)
        {
            await _db.Indeksacje.AddAsync(entity, ct);
        }

        public Task<Indeksacja?> GetByKodAsync(string kod, CancellationToken ct)
        {
            return _db.Indeksacje.FirstOrDefaultAsync(x => x.KodIndeksacji == kod, ct);
        }

        public Task<List<Indeksacja>> GetAllAsync(CancellationToken ct)
        {
            return _db.Indeksacje.OrderBy(x => x.KodIndeksacji).ToListAsync(ct);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
        public Task RemoveAsync(Indeksacja entity, CancellationToken ct)
        {
            _db.Indeksacje.Remove(entity);
            return Task.CompletedTask;
        }

    }
}
