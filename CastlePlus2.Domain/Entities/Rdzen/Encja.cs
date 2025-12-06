using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CastlePlus2.Domain.Entities.Rdzen
{
    [Table("Encja", Schema = "rdzen")]
    public class Encja
    {
        [Key]
        [Column("IdEncji")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]
        [Column("TypEncji")]
        public string TypEncji { get; set; } = string.Empty;

        [MaxLength(40)]
        [Column("KodEncji")]
        public string? KodEncji { get; set; }

        [Column("UtworzonoUTC")]
        public DateTime UtworzonoUtc { get; set; }

        [Column("ZmienionoUTC")]
        public DateTime ZmienionoUtc { get; set; }

        [Timestamp]
        [Column("RowVersion")]
        public byte[]? RowVersion { get; set; }
    }
}