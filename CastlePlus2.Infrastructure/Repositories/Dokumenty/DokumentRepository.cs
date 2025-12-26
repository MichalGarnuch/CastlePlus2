// PLIK: CastlePlus2.Infrastructure/Repositories/Dokumenty/DokumentRepository.cs
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Domain.Entities.Dokumenty;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Dokumenty
{
    public class DokumentRepository : IDokumentRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public DokumentRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dokument?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Dokumenty
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.IdDokumentu == id, cancellationToken);
        }

        public async Task<Dokument?> GetForUpdateAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Dokumenty
                .FirstOrDefaultAsync(d => d.IdDokumentu == id, cancellationToken);
        }

        public async Task<List<Dokument>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Dokumenty
                .AsNoTracking()
                .OrderByDescending(d => d.IdDokumentu)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Dokument entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Dokumenty.AddAsync(entity, cancellationToken);
        }

        public Task RemoveAsync(Dokument entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Dokumenty.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
