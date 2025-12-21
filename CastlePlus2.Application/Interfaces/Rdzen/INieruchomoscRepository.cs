using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Application.Interfaces.Rdzen
{
    /// <summary>
    /// Interfejs repozytorium dla encji Nieruchomosc.
    /// 
    /// Idea:
    /// - Warstwa Application nie zna szczegółów EF Core ani SQL Server.
    /// - Komunikuje się z danymi przez wyspecjalizowane repozytoria.
    /// - Implementacja tego interfejsu będzie w warstwie Infrastructure.
    /// 
    /// Dlaczego nie generyczne IRepository<T>?
    /// - Korzystamy z prostego, dedykowanego interfejsu – tylko to, czego faktycznie
    ///   potrzebuje logika biznesowa (CQRS).
    /// - Bez nadmiarowej abstrakcji, ale z zachowaniem rozdziału warstw.
    /// </summary>
    public interface INieruchomoscRepository
    {
        /// <summary>
        /// Pobiera nieruchomość po Id (GUID).
        /// Zwraca null, jeżeli nie znaleziono.
        /// </summary>
        Task<Nieruchomosc?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Dodaje nową nieruchomość do kontekstu (bez zapisywania zmian w bazie).
        /// Rzeczywisty zapis następuje w SaveChangesAsync.
        /// </summary>
        Task AddAsync(Nieruchomosc entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Zapisuje wszystkie zmiany wykonane w repozytorium do bazy danych.
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<List<Nieruchomosc>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Nieruchomosc?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken = default);
        void Remove(Nieruchomosc entity);

    }
}

