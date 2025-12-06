using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace CastlePlus2.Domain.Entities.Rdzen
{
    [Table("Nieruchomosc", Schema = "rdzen")]
    public class Nieruchomosc : Encja
    {
        public Nieruchomosc()
        {
            TypEncji = "NIERUCHOMOSC";
        }

        [Required]
        [MaxLength(200)]
        public string Nazwa { get; set; } = string.Empty;

        [Column("IdAdresuGlownego")]
        public long? IdAdresuGlownego { get; set; }

        [Column("IdPodmiotuWlasciciela")]
        public long? IdPodmiotuWlasciciela { get; set; }

        [Column(TypeName = "geography")]
        public Geometry? Geometria { get; set; }
    }
}