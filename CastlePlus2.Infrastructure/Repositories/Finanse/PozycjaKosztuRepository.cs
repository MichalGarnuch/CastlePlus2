// PLIK: CastlePlus2.Infrastructure/Repositories/Finanse/PozycjaKosztuRepository.cs
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

        public async Task<PozycjaKosztu?> GetForUpdateAsync(long id, CancellationToken ct)
        {
            return await _db.PozycjeKosztow
                .FirstOrDefaultAsync(x => x.IdPozycjiKosztu == id, ct);
        }

        public async Task<List<PozycjaKosztu>> GetAllAsync(CancellationToken ct)
        {
            return await _db.PozycjeKosztow
                .AsNoTracking()
                .OrderByDescending(x => x.IdPozycjiKosztu)
                .ToListAsync(ct);
        }

        public async Task AddAsync(PozycjaKosztu entity, CancellationToken ct)
        {
            await _db.PozycjeKosztow.AddAsync(entity, ct);
        }

        public Task RemoveAsync(PozycjaKosztu entity, CancellationToken ct)
        {
            _db.PozycjeKosztow.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
