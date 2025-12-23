using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Rdzen
{
    public class EncjaRepository : IEncjaRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public EncjaRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Encja?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Encje
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<List<Encja>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Encje
                .AsNoTracking()
                .OrderByDescending(e => e.UtworzonoUtc)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Encja entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Encje.AddAsync(entity, cancellationToken);
        }

        public async Task<Encja?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Encje
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public void Remove(Encja entity)
        {
            _dbContext.Encje.Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}