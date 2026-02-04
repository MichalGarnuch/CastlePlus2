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

        public async Task<(List<Podmiot> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            string? searchTerm,
            string? sortBy,
            bool sortDesc,
            CancellationToken ct)
        {
            var query = _db.Podmioty.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim();
                query = query.Where(x =>
                    x.Nazwa.Contains(term)
                    || x.TypPodmiotu.Contains(term)
                    || (x.NIP != null && x.NIP.Contains(term))
                    || (x.REGON != null && x.REGON.Contains(term))
                    || (x.PESEL != null && x.PESEL.Contains(term)));
            }

            query = sortBy switch
            {
                "Nazwa" => sortDesc ? query.OrderByDescending(x => x.Nazwa) : query.OrderBy(x => x.Nazwa),
                "TypPodmiotu" => sortDesc ? query.OrderByDescending(x => x.TypPodmiotu) : query.OrderBy(x => x.TypPodmiotu),
                "NIP" => sortDesc ? query.OrderByDescending(x => x.NIP) : query.OrderBy(x => x.NIP),
                "REGON" => sortDesc ? query.OrderByDescending(x => x.REGON) : query.OrderBy(x => x.REGON),
                "PESEL" => sortDesc ? query.OrderByDescending(x => x.PESEL) : query.OrderBy(x => x.PESEL),
                _ => sortDesc ? query.OrderByDescending(x => x.IdPodmiotu) : query.OrderBy(x => x.IdPodmiotu)
            };

            var total = await query.CountAsync(ct);
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);

            return (items, total);
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
