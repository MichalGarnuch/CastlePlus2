using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Rdzen
{
    public class UpdateEncjaRequest
    {
        [Required, MaxLength(40)]
        public string TypEncji { get; set; } = string.Empty;

        [MaxLength(40)]
        public string? KodEncji { get; set; }
    }
}