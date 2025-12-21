using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.DTOs.Slowniki
{
    public class CreateWalutaRequest
    {
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string KodWaluty { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Nazwa { get; set; } = string.Empty;
    }
}
