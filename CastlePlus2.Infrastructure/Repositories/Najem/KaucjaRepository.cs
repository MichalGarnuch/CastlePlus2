using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Domain.Entities.Najem;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Najem
{
    public class KaucjaRepository : IKaucjaRepository
    {
        private readonly CastlePlus2DbContext _db;

        public KaucjaRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Kaucja entity, CancellationToken ct)
        {
            await _db.Kaucje.AddAsync(entity, ct);
        }

        public Task<Kaucja?> GetByIdAsync(long id, CancellationToken ct)
        {
            return _db.Kaucje.FirstOrDefaultAsync(x => x.IdKaucji == id, ct);
        }

        public Task<List<Kaucja>> GetAllAsync(CancellationToken ct)
        {
            return _db.Kaucje.OrderByDescending(x => x.IdKaucji).ToListAsync(ct);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
