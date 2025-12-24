using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Najem
{
    public class UpdateSkladnikCzynszuRequest
    {
        [Required]
        public Guid IdUmowyNajmu { get; set; }

        [Required, StringLength(100)]
        public string Nazwa { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string KodJednostki { get; set; } = string.Empty;

        [Required]
        public decimal Stawka { get; set; }

        public decimal? IloscBazowa { get; set; }

        [StringLength(20)]
        public string? KodIndeksacji { get; set; }

        [Required]
        public DateOnly OdDnia { get; set; }

        public DateOnly? DoDnia { get; set; }
    }
}