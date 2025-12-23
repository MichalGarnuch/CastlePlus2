using System;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Finanse
{
    public class AlokacjaKosztuRepository : IAlokacjaKosztuRepository
    {
        private readonly CastlePlus2DbContext _db;

        public AlokacjaKosztuRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<AlokacjaKosztu?> GetByIdAsync(long idAlokacji, CancellationToken ct)
        {
            return await _db.AlokacjeKosztow
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdAlokacji == idAlokacji, ct);
        }

        public async Task<AlokacjaKosztu?> GetForUpdateAsync(long idAlokacji, CancellationToken ct)
        {
            return await _db.AlokacjeKosztow
                .FirstOrDefaultAsync(x => x.IdAlokacji == idAlokacji, ct);
        }

        public async Task<List<AlokacjaKosztu>> GetAllAsync(CancellationToken ct)
        {
            return await _db.AlokacjeKosztow
                .AsNoTracking()
                .OrderByDescending(x => x.IdAlokacji)
                .ToListAsync(ct);
        }

        public async Task<bool> ExistsAsync(long idPozycjiKosztu, Guid idEncji, CancellationToken ct)
        {
            return await _db.AlokacjeKosztow
                .AsNoTracking()
                .AnyAsync(x => x.IdPozycjiKosztu == idPozycjiKosztu && x.IdEncji == idEncji, ct);
        }

        public async Task<bool> ExistsOtherAsync(long idPozycjiKosztu, Guid idEncji, long excludeIdAlokacji, CancellationToken ct)
        {
            return await _db.AlokacjeKosztow
                .AsNoTracking()
                .AnyAsync(x =>
                    x.IdPozycjiKosztu == idPozycjiKosztu
                    && x.IdEncji == idEncji
                    && x.IdAlokacji != excludeIdAlokacji, ct);
        }

        public async Task<bool> EncjaExistsAsync(Guid idEncji, CancellationToken ct)
        {
            return await _db.Encje
                .AsNoTracking()
                .AnyAsync(x => x.Id == idEncji, ct);
        }

        public async Task<bool> PozycjaKosztuExistsAsync(long idPozycjiKosztu, CancellationToken ct)
        {
            return await _db.PozycjeKosztow
                .AsNoTracking()
                .AnyAsync(x => x.IdPozycjiKosztu == idPozycjiKosztu, ct);
        }

        public async Task AddAsync(AlokacjaKosztu entity, CancellationToken ct)
        {
            await _db.AlokacjeKosztow.AddAsync(entity, ct);
        }

        public Task RemoveAsync(AlokacjaKosztu entity, CancellationToken ct)
        {
            _db.AlokacjeKosztow.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
