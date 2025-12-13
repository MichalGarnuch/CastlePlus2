using System;

namespace CastlePlus2.Contracts.DTOs.Finanse
{
    public class FakturaDto
    {
        public long IdFaktury { get; set; }
        public string NumerFaktury { get; set; } = string.Empty;
        public long IdPodmiotu { get; set; }
        public DateTime DataWystawienia { get; set; }
        public DateTime? DataSprzedazy { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public decimal? KwotaNetto { get; set; }
        public decimal? KwotaBrutto { get; set; }
    }
}
