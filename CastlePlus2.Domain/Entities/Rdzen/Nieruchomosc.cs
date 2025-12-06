using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastlePlus2.Domain.Common;
using NetTopologySuite.Geometries; // Biblioteka do obsługi typu [geography]
using System.ComponentModel.DataAnnotations.Schema;

namespace CastlePlus2.Domain.Entities.Rdzen
{
    /// <summary>
    /// Reprezentuje tabelę [rdzen].[Nieruchomosc].
    /// Jest to główna jednostka grupująca budynki i grunty.
    /// </summary>
    [Table("Nieruchomosc", Schema = "rdzen")] // Jawne wskazanie tabeli i schematu
    public class Nieruchomosc : GuidEntity
    {
        /// <summary>
        /// Klucz główny (PK) - w bazie [IdEncji] [uniqueidentifier].
        /// Nadpisujemy właściwość Id z klasy bazowej.
        /// </summary>
        [Column("IdEncji")] // Mapowanie na nazwę kolumny w SQL
        public override Guid Id { get; set; }

        /// <summary>
        /// Nazwa nieruchomości (np. "Osiedle Słoneczne").
        /// SQL: [Nazwa] [nvarchar](200) NOT NULL
        /// </summary>
        public string Nazwa { get; set; } = string.Empty; // Inicjalizacja, żeby uniknąć null warnings

        /// <summary>
        /// Klucz obcy do adresu głównego.
        /// SQL: [IdAdresuGlownego] [bigint] NULL
        /// </summary>
        public long? IdAdresuGlownego { get; set; }

        /// <summary>
        /// Klucz obcy do właściciela (Podmiot).
        /// SQL: [IdPodmiotuWlasciciela] [bigint] NULL
        /// </summary>
        public long? IdPodmiotuWlasciciela { get; set; }

        /// <summary>
        /// Lokalizacja geograficzna (współrzędne).
        /// SQL: [Geometria] [geography] NULL
        /// Wymaga biblioteki NetTopologySuite.
        /// </summary>
        public Geometry? Geometria { get; set; }

        // Uwaga: RowVersion jest dziedziczone z GuidEntity -> BaseEntity

        // --- Relacje (Navigation Properties) ---
        // Dodamy je, gdy utworzymy klasy Adres i Podmiot. 
        // Na razie zostawiamy zakomentowane, żeby kod się kompilował.

        // public virtual Adres? AdresGlowny { get; set; }
        // public virtual Podmiot? Wlasciciel { get; set; }
        // public virtual ICollection<Budynek> Budynki { get; set; } = new List<Budynek>();
    }
}
