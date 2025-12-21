using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Rdzen
{
    /// <summary>
    /// Implementacja repozytorium budynków oparta o EF Core.
    /// </summary>
    public class BudynekRepository : IBudynekRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public BudynekRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Budynek?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Budynki
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            // Uwaga: b.Id pochodzi z klasy bazowej Encja (mapowane na kolumnę IdEncji).
        }

        public async Task AddAsync(Budynek entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Budynki.AddAsync(entity, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<Budynek>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Budynki
                .AsNoTracking()
                .OrderBy(b => b.KodBudynku)
                .ToListAsync(cancellationToken);
        }

        public async Task<Budynek?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Budynki
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public void Remove(Budynek entity)
        {
            _dbContext.Budynki.Remove(entity);
        }

    }
}
