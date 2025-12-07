using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CastlePlus2.Domain.Entities.Rdzen
{
    /// <summary>
    /// Encja domenowa odzwierciedlająca tabelę [rdzen].[Adres].
    /// Uwaga: celowo NIE dziedziczymy z LongEntity, bo tabela nie ma kolumny RowVersion.
    /// </summary>
    [Table("Adres", Schema = "rdzen")]
    public class Adres
    {
        // Klucz główny zgodny z SQL: [IdAdresu] [bigint] IDENTITY(1,1)
        [Key]
        [Column("IdAdresu")]
        public long IdAdresu { get; set; }

        [Required]
        [MaxLength(60)]
        public string Kraj { get; set; } = "Polska";

        [MaxLength(60)]
        public string? Wojewodztwo { get; set; }

        [MaxLength(60)]
        public string? Powiat { get; set; }

        [MaxLength(60)]
        public string? Gmina { get; set; }

        [Required]
        [MaxLength(100)]
        public string Miejscowosc { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Ulica { get; set; }

        [MaxLength(20)]
        public string? Numer { get; set; }

        [MaxLength(12)]
        public string? KodPocztowy { get; set; }

        [MaxLength(300)]
        public string? AdresPelny { get; set; }
    }
}

