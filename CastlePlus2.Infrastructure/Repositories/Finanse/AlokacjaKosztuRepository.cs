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

        public async Task<bool> ExistsAsync(long idPozycjiKosztu, Guid idEncji, CancellationToken ct)
        {
            return await _db.AlokacjeKosztow
                .AsNoTracking()
                .AnyAsync(x => x.IdPozycjiKosztu == idPozycjiKosztu && x.IdEncji == idEncji, ct);
        }

        public async Task AddAsync(AlokacjaKosztu entity, CancellationToken ct)
        {
            await _db.AlokacjeKosztow.AddAsync(entity, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
