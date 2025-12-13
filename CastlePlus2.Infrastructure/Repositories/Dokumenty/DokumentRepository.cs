using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Domain.Entities.Dokumenty;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Dokumenty
{
    /// <summary>
    /// Repozytorium EF Core dla encji Dokument.
    /// </summary>
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

        public async Task AddAsync(Dokument entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Dokumenty.AddAsync(entity, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
