using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Domain.Entities.Slowniki;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Slowniki
{
    public class JednostkaMiaryRepository : IJednostkaMiaryRepository
    {
        private readonly CastlePlus2DbContext _db;

        public JednostkaMiaryRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(JednostkaMiary entity, CancellationToken ct)
        {
            await _db.JednostkiMiary.AddAsync(entity, ct);
        }

        public Task<JednostkaMiary?> GetByKodAsync(string kod, CancellationToken ct)
        {
            return _db.JednostkiMiary.FirstOrDefaultAsync(x => x.KodJednostki == kod, ct);
        }

        public Task<List<JednostkaMiary>> GetAllAsync(CancellationToken ct)
        {
            return _db.JednostkiMiary.OrderBy(x => x.KodJednostki).ToListAsync(ct);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
