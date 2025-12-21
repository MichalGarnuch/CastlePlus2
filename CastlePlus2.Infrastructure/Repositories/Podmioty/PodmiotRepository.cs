using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Domain.Entities.Podmioty;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Podmioty
{
    public class PodmiotRepository : IPodmiotRepository
    {
        private readonly CastlePlus2DbContext _db;

        public PodmiotRepository(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Podmiot podmiot, CancellationToken ct)
        {
            await _db.Podmioty.AddAsync(podmiot, ct);
        }

        public Task<Podmiot?> GetByIdAsync(long idPodmiotu, CancellationToken ct)
        {
            return _db.Podmioty.FirstOrDefaultAsync(x => x.IdPodmiotu == idPodmiotu, ct);
        }

        public Task<List<Podmiot>> GetAllAsync(CancellationToken ct)
        {
            return _db.Podmioty.OrderBy(x => x.IdPodmiotu).ToListAsync(ct);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }

        public void Remove(Podmiot podmiot)
        {
            _db.Podmioty.Remove(podmiot);
        }

    }
}
