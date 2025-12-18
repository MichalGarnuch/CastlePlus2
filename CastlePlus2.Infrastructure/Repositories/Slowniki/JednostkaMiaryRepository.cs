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

        public Task<List<JednostkaMiary>> GetAllAsync(CancellationToken ct)
            => _db.JednostkiMiary.AsNoTracking().OrderBy(x => x.KodJednostki).ToListAsync(ct);

        public Task<JednostkaMiary?> GetByKodAsync(string kodJednostki, CancellationToken ct)
            => _db.JednostkiMiary.FirstOrDefaultAsync(x => x.KodJednostki == kodJednostki, ct);

        public Task<bool> ExistsAsync(string kodJednostki, CancellationToken ct)
            => _db.JednostkiMiary.AnyAsync(x => x.KodJednostki == kodJednostki, ct);

        public Task AddAsync(JednostkaMiary entity, CancellationToken ct)
            => _db.JednostkiMiary.AddAsync(entity, ct).AsTask();

        public void Remove(JednostkaMiary entity)
            => _db.JednostkiMiary.Remove(entity);

        public Task<int> SaveChangesAsync(CancellationToken ct)
            => _db.SaveChangesAsync(ct);
    }
}
