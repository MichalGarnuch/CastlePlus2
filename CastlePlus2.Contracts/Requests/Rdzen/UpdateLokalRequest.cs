using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Rdzen
{
    public class UpdateLokalRequest
    {
        [Required]
        public Guid IdBudynku { get; set; }

        [Required, MaxLength(50)]
        public string KodLokalu { get; set; } = string.Empty;

        public decimal? Powierzchnia { get; set; }

        [MaxLength(60)]
        public string? Przeznaczenie { get; set; }
    }
}
