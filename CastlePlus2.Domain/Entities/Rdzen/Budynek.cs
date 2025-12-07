using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CastlePlus2.Domain.Entities.Rdzen
{
    /// <summary>
    /// Encja domenowa odzwierciedlająca tabelę [rdzen].[Budynek].
    /// Dziedziczy po Encja (Id, TypEncji, RowVersion itd.).
    /// </summary>
    [Table("Budynek", Schema = "rdzen")]
    public class Budynek : Encja
    {
        public Budynek()
        {
            // Stały typ encji dla budynków – spójność z rdzeniem.
            TypEncji = "BUDYNEK";
        }

        /// <summary>
        /// Id nieruchomości, do której należy budynek (FK do [rdzen].[Nieruchomosc].[IdEncji]).
        /// </summary>
        [Required]
        [Column("IdNieruchomosci")]
        public Guid IdNieruchomosci { get; set; }

        /// <summary>
        /// Nawigacja do nieruchomości – przydatne przy JOIN-ach w EF.
        /// </summary>
        [ForeignKey(nameof(IdNieruchomosci))]
        public Nieruchomosc Nieruchomosc { get; set; } = null!;

        /// <summary>
        /// Kod budynku na nieruchomości, np. "A", "B1".
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string KodBudynku { get; set; } = string.Empty;

        /// <summary>
        /// Opcjonalny Id adresu budynku (FK do [rdzen].[Adres].[IdAdresu]).
        /// </summary>
        [Column("IdAdresu")]
        public long? IdAdresu { get; set; }

        /// <summary>
        /// Nawigacja do adresu budynku.
        /// </summary>
        [ForeignKey(nameof(IdAdresu))]
        public Adres? Adres { get; set; }

        /// <summary>
        /// Łączna liczba kondygnacji (nad + podziemnych) – odpowiada kolumnie [Kondygnacje] smallint NULL.
        /// </summary>
        [Column("Kondygnacje")]
        public short? Kondygnacje { get; set; }

        /// <summary>
        /// Powierzchnia użytkowa [m2], decimal(12,2) w SQL.
        /// </summary>
        [Column("PowierzchniaUzytkowa", TypeName = "decimal(12,2)")]
        public decimal? PowierzchniaUzytkowa { get; set; }
    }
}
