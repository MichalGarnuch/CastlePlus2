using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Rdzen
{
    public class AdresRepository : IAdresRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public AdresRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Adres?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Adresy
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdAdresu == id, cancellationToken);
        }

        public async Task<List<Adres>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Adresy
                .AsNoTracking()
                .OrderByDescending(a => a.IdAdresu)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Adres entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Adresy.AddAsync(entity, cancellationToken);
        }

        public Task UpdateAsync(Adres entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Adresy.Update(entity);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Adres entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Adresy.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
