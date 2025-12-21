using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.DTOs.Slowniki
{
    public class UpdateWalutaRequest
    {
        [Required]
        [StringLength(50)]
        public string Nazwa { get; set; } = string.Empty;
    }
}
