using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastlePlus2.Domain.Common
{
    /// <summary>
    /// Generyczna klasa bazowa dla wszystkich encji w systemie.
    /// Pozwala na ujednolicenie obsługi kluczy głównych (GUID lub long).
    /// </summary>
    /// <typeparam name="TKey">Typ klucza głównego (Guid lub long)</typeparam>
    public abstract class BaseEntity<TKey>
    {
        // Uwaga: W konkretnych klasach będziemy mapować to na odpowiednią kolumnę SQL (np. IdEncji lub IdFaktury)
        // Tutaj definiujemy tylko abstrakcyjny getter, żeby wymusić istnienie ID.
        public abstract TKey Id { get; set; }

        /// <summary>
        /// Obsługa RowVersion (Optimistic Concurrency) - kolumna [timestamp] w bazie.
        /// Zapobiega nadpisaniu danych, jeśli ktoś inny zmienił je w międzyczasie.
        /// </summary>
        public byte[]? RowVersion { get; set; }
    }
}
