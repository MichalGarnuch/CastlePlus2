using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Rdzen
{
    public class CreatePrzypisanieAdresuRequest
    {
        [Required]
        public Guid IdEncji { get; set; }

        [Required]
        public long IdAdresu { get; set; }

        [Required]
        public DateOnly OdDnia { get; set; }
    }
}
