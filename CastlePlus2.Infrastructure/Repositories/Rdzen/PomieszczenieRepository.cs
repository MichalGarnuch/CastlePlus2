using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Rdzen
{
    /// <summary>
    /// Repozytorium EF Core dla Pomieszczenie.
    /// </summary>
    public class PomieszczenieRepository : IPomieszczenieRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public PomieszczenieRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Pomieszczenie?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Pomieszczenia
                .AsNoTracking()
                .Include(p => p.Lokal)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Pomieszczenie>> GetByLokalAsync(Guid idEncjiLokal, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Pomieszczenia
                .AsNoTracking()
                .Where(p => p.IdEncjiNadrzednej == idEncjiLokal)
                .OrderBy(p => p.KodPomieszczenia)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsWithCodeForLokalAsync(Guid idEncjiLokal, string kodPomieszczenia, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Pomieszczenia
                .AnyAsync(
                    p => p.IdEncjiNadrzednej == idEncjiLokal && p.KodPomieszczenia == kodPomieszczenia,
                    cancellationToken);
        }

        public async Task AddAsync(Pomieszczenie entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Pomieszczenia.AddAsync(entity, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        // DODANE pod pełny CRUD:

        public async Task<List<Pomieszczenie>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Pomieszczenia
                .AsNoTracking()
                .OrderBy(x => x.KodPomieszczenia)
                .ToListAsync(cancellationToken);
        }

        public async Task<Pomieszczenie?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Pomieszczenia
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Remove(Pomieszczenie entity)
        {
            _dbContext.Pomieszczenia.Remove(entity);
        }
    }
}
