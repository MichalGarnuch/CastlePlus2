using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Domain.Entities.Najem;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Najem
{
    public class UmowaNajmuRepository : IUmowaNajmuRepository
    {
        private readonly CastlePlus2DbContext _db;

        public UmowaNajmuRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(UmowaNajmu entity, CancellationToken ct)
        {
            await _db.UmowyNajmu.AddAsync(entity, ct);
        }

        public Task<UmowaNajmu?> GetByIdAsync(Guid idEncji, CancellationToken ct)
        {
            return _db.UmowyNajmu.FirstOrDefaultAsync(x => x.Id == idEncji, ct);
        }

        public Task<List<UmowaNajmu>> GetAllAsync(CancellationToken ct)
        {
            return _db.UmowyNajmu.OrderByDescending(x => x.UtworzonoUtc).ToListAsync(ct);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
