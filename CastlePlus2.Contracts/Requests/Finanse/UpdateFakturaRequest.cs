using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Finanse
{
    public class UpdateFakturaRequest
    {
        [Required, MaxLength(60)]
        public string NumerFaktury { get; set; } = string.Empty;

        [Range(1, long.MaxValue)]
        public long IdPodmiotu { get; set; }

        public DateTime DataWystawienia { get; set; }

        public DateTime? DataSprzedazy { get; set; }

        [Required, StringLength(3, MinimumLength = 3)]
        public string KodWaluty { get; set; } = string.Empty;

        public decimal? KwotaNetto { get; set; }
        public decimal? KwotaBrutto { get; set; }
    }
}
