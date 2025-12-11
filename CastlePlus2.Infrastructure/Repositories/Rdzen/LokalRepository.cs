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
    /// Implementacja ILokalRepository oparta o EF Core.
    /// </summary>
    public class LokalRepository : ILokalRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public LokalRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Lokal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Lokale
                .Include(l => l.Budynek)
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Lokal>> GetByBudynekAsync(
            Guid idBudynku,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Lokale
                .Where(l => l.IdBudynku == idBudynku)
                .OrderBy(l => l.KodLokalu)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsWithCodeForBudynekAsync(
            Guid idBudynku,
            string kodLokalu,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Lokale
                .AnyAsync(
                    l => l.IdBudynku == idBudynku && l.KodLokalu == kodLokalu,
                    cancellationToken);
        }

        public async Task AddAsync(Lokal entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Lokale.AddAsync(entity, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
