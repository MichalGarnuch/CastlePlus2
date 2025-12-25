using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using CastlePlus2.Domain.Entities.Utrzymanie;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Utrzymanie
{
    /// <summary>
    /// Repozytorium EF Core dla zleceń pracy.
    /// </summary>
    public class ZleceniePracyRepository : IZleceniePracyRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public ZleceniePracyRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ZleceniePracy?> GetByIdAsync(long idZlecenia, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ZleceniaPracy
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdZlecenia == idZlecenia, cancellationToken);
        }

        public async Task<ZleceniePracy?> GetForUpdateAsync(long idZlecenia, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ZleceniaPracy
                .FirstOrDefaultAsync(x => x.IdZlecenia == idZlecenia, cancellationToken);
        }

        public async Task<List<ZleceniePracy>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.ZleceniaPracy
                .AsNoTracking()
                .OrderByDescending(x => x.IdZlecenia)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(ZleceniePracy entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.ZleceniaPracy.AddAsync(entity, cancellationToken);
        }

        public Task RemoveAsync(ZleceniePracy entity, CancellationToken cancellationToken = default)
        {
            _dbContext.ZleceniaPracy.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}