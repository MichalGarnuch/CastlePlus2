using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Finanse
{
    public class CreateRozliczeniePlatnosciRequest
    {
        [Required]
        public long IdPlatnosci { get; set; }

        [Required]
        public long IdFaktury { get; set; }

        [Required]
        public decimal Kwota { get; set; }
    }
}
