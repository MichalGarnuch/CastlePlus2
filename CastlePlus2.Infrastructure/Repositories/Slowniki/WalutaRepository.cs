using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Domain.Entities.Slowniki;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Slowniki
{
    public class WalutaRepository : IWalutaRepository
    {
        private readonly CastlePlus2DbContext _db;

        public WalutaRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Waluta waluta, CancellationToken ct)
        {
            await _db.Waluty.AddAsync(waluta, ct);
        }

        public Task<Waluta?> GetByKodAsync(string kodWaluty, CancellationToken ct)
        {
            return _db.Waluty.FirstOrDefaultAsync(x => x.KodWaluty == kodWaluty, ct);
        }

        public Task<List<Waluta>> GetAllAsync(CancellationToken ct)
        {
            return _db.Waluty.OrderBy(x => x.KodWaluty).ToListAsync(ct);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
