using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Najem
{
    public class UpdateUmowaNajmuRequest
    {
        [Required]
        public long IdWynajmujacego { get; set; }

        [Required]
        public long IdNajemcy { get; set; }

        [Required]
        public DateTime DataZawarcia { get; set; }

        [Required]
        public DateTime DataPoczatku { get; set; }

        public DateTime? DataZakonczenia { get; set; }

        [Required, MaxLength(3)]
        public string KodWaluty { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? KodIndeksacji { get; set; }
    }
}