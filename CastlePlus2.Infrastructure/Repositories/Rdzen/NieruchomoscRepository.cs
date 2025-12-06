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
    /// 
    /// Zasada:
    /// - Ta klasa "zna" CastlePlus2DbContext i EF Core.
    /// - Warstwa Application komunikuje się z danymi tylko przez interfejs INieruchomoscRepository.
    /// - Dzięki temu możemy łatwiej testować logikę (mockujemy interfejs),
    ///   a szczegóły dostępu do danych są zamknięte w Infrastructure.
    /// </summary>
    public class NieruchomoscRepository : INieruchomoscRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        /// <summary>
        /// Repozytorium przyjmuje w konstruktorze CastlePlus2DbContext przez DI.
        /// </summary>
        /// <param name="dbContext">Kontekst EF Core połączony z bazą CastlePlus2.</param>
        public NieruchomoscRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Nieruchomosc?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // Używamy AsNoTracking jeśli nie planujemy modyfikować pobranego obiektu.
            // Przy odczycie do DTO jest to optymalniejsze (mniej obciążenia change trackera).
            return await _dbContext.Nieruchomosci
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task AddAsync(Nieruchomosc entity, CancellationToken cancellationToken = default)
        {
            // Dodajemy encję do kontekstu – nie zapisuje to jeszcze zmian w bazie.
            await _dbContext.Nieruchomosci.AddAsync(entity, cancellationToken);
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Zapisujemy wszystkie zmiany wprowadzone w kontekście do bazy danych.
            //
            // Uwaga:
            // - W przypadku konfliktu RowVersion EF Core rzuci DbUpdateConcurrencyException.
            // - Obsługę tego wyjątku zrobimy w handlerach (logika aplikacyjna).
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

