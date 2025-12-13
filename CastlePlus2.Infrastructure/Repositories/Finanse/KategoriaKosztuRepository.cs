using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Finanse
{
    public class KategoriaKosztuRepository : IKategoriaKosztuRepository
    {
        private readonly CastlePlus2DbContext _db;

        public KategoriaKosztuRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<KategoriaKosztu?> GetByIdAsync(long id, CancellationToken ct)
        {
            return await _db.KategorieKosztow
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdKategoriiKosztu == id, ct);
        }

        public async Task<bool> ExistsByKodAsync(string kod, CancellationToken ct)
        {
            return await _db.KategorieKosztow
                .AsNoTracking()
                .AnyAsync(x => x.Kod == kod, ct);
        }

        public async Task AddAsync(KategoriaKosztu entity, CancellationToken ct)
        {
            await _db.KategorieKosztow.AddAsync(entity, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
