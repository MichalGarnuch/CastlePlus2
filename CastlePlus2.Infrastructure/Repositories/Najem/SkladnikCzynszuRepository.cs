using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Domain.Entities.Najem;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Najem
{
    public class SkladnikCzynszuRepository : ISkladnikCzynszuRepository
    {
        private readonly CastlePlus2DbContext _db;

        public SkladnikCzynszuRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(SkladnikCzynszu entity, CancellationToken ct)
        {
            await _db.SkladnikiCzynszu.AddAsync(entity, ct);
        }

        public Task<SkladnikCzynszu?> GetByIdAsync(long id, CancellationToken ct)
        {
            return _db.SkladnikiCzynszu.FirstOrDefaultAsync(x => x.IdSkladnikaCzynszu == id, ct);
        }

        public Task<List<SkladnikCzynszu>> GetAllAsync(CancellationToken ct)
        {
            return _db.SkladnikiCzynszu.OrderByDescending(x => x.IdSkladnikaCzynszu).ToListAsync(ct);
        }
        public void Remove(SkladnikCzynszu entity)
        {
            _db.SkladnikiCzynszu.Remove(entity);
        }
        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
