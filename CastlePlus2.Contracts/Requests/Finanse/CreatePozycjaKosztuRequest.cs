using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Finanse
{
    public class CreatePozycjaKosztuRequest
    {
        [Required]
        public long IdFaktury { get; set; }

        [Required]
        public long IdKategoriiKosztu { get; set; }

        [MaxLength(200)]
        public string? Opis { get; set; }

        [Required]
        public decimal KwotaNetto { get; set; }

        [Required]
        public decimal KwotaBrutto { get; set; }
    }
}
