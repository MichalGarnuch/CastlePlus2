using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Rdzen
{
    public class CreateNieruchomoscRequest
    {
        [Required]
        [MaxLength(200)]
        public string Nazwa { get; set; } = string.Empty;

        public long? IdAdresuGlownego { get; set; }

        public long? IdPodmiotuWlasciciela { get; set; }
    }
}
