using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Rdzen
{
    public class UpdatePrzypisanieAdresuRequest
    {
        [Required]
        public long IdAdresu { get; set; }

        [Required]
        public DateOnly OdDnia { get; set; }

        public DateOnly? DoDnia { get; set; }
    }
}
