using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Rdzen
{
    public class UpdateBudynekRequest
    {
        [Required]
        public Guid IdNieruchomosci { get; set; }

        [Required, MaxLength(50)]
        public string KodBudynku { get; set; } = string.Empty;

        public long? IdAdresu { get; set; }

        public short? Kondygnacje { get; set; }
        public decimal? PowierzchniaUzytkowa { get; set; }
    }
}
