using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Finanse
{
    public class PozycjaKosztuRepository : IPozycjaKosztuRepository
    {
        private readonly CastlePlus2DbContext _db;

        public PozycjaKosztuRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<PozycjaKosztu?> GetByIdAsync(long id, CancellationToken ct)
        {
            return await _db.PozycjeKosztow
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdPozycjiKosztu == id, ct);
        }

        public async Task AddAsync(PozycjaKosztu entity, CancellationToken ct)
        {
            await _db.PozycjeKosztow.AddAsync(entity, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
