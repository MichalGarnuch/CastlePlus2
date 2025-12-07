using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Rdzen
{
    /// <summary>
    /// Implementacja repozytorium dla encji Nieruchomosc oparta o EF Core.
    /// </summary>
    public class NieruchomoscRepository : INieruchomoscRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public NieruchomoscRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Nieruchomosc?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // AsNoTracking – bo wynik mapujemy do DTO, nie będziemy go modyfikować.
            // Include – dociągamy AdresGlowny w jednym zapytaniu SQL (JOIN).
            return await _dbContext.Nieruchomosci
                .Include(n => n.AdresGlowny)
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task AddAsync(Nieruchomosc entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Nieruchomosci.AddAsync(entity, cancellationToken);
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
