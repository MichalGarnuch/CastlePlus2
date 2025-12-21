using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Podmioty
{
    public class UpdatePodmiotRequest
    {
        [Required, MaxLength(200)]
        public string Nazwa { get; set; } = default!;

        [MaxLength(20)]
        public string? NIP { get; set; }

        [MaxLength(20)]
        public string? REGON { get; set; }

        [MaxLength(11)]
        public string? PESEL { get; set; }

        [Required, MaxLength(30)]
        public string TypPodmiotu { get; set; } = default!;
    }
}
