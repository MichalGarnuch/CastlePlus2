using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CastlePlus2.Domain.Entities.Rdzen
{
    /// <summary>
    /// Encja domenowa dla tabeli [rdzen].[Pomieszczenie].
    /// PK = Id (mapowane na kolumnę [IdEncji] przez Encja).
    /// </summary>
    [Table("Pomieszczenie", Schema = "rdzen")]
    public class Pomieszczenie : Encja
    {
        public Pomieszczenie()
        {
            // Spójność z rdzeniem – stały typ encji
            TypEncji = "POMIESZCZENIE";
        }

        /// <summary>
        /// Id encji nadrzędnej (lokalu), FK do [rdzen].[Lokal].[IdEncji].
        /// W bazie: [IdEncjiNadrzednej] UNIQUEIDENTIFIER NOT NULL.
        /// </summary>
        [Required]
        [Column("IdEncjiNadrzednej")]
        public Guid IdEncjiNadrzednej { get; set; }

        /// <summary>
        /// Kod pomieszczenia, np. "P1", "P2".
        /// W bazie: [KodPomieszczenia] NVARCHAR(50) NOT NULL.
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Column("KodPomieszczenia")]
        public string KodPomieszczenia { get; set; } = string.Empty;

        /// <summary>
        /// Powierzchnia w m2 (kolumna [Powierzchnia] DECIMAL(12,2) NULL).
        /// </summary>
        [Column("Powierzchnia", TypeName = "decimal(12,2)")]
        public decimal? Powierzchnia { get; set; }

        /// <summary>
        /// Nawigacja do lokalu nadrzędnego.
        /// </summary>
        [ForeignKey(nameof(IdEncjiNadrzednej))]
        public Lokal Lokal { get; set; } = null!;
    }
}
