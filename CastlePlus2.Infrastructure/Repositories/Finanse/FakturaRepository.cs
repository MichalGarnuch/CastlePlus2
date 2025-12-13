using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Finanse
{
    public class FakturaRepository : IFakturaRepository
    {
        private readonly CastlePlus2DbContext _db;

        public FakturaRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<Faktura?> GetByIdAsync(long idFaktury, CancellationToken ct)
        {
            return await _db.Faktury
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdFaktury == idFaktury, ct);
        }

        public async Task<bool> ExistsByNumerAsync(string numerFaktury, CancellationToken ct)
        {
            return await _db.Faktury
                .AsNoTracking()
                .AnyAsync(x => x.NumerFaktury == numerFaktury, ct);
        }

        public async Task AddAsync(Faktura entity, CancellationToken ct)
        {
            await _db.Faktury.AddAsync(entity, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
