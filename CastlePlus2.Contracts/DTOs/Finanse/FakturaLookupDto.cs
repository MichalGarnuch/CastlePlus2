using System;

namespace CastlePlus2.Contracts.DTOs.Finanse
{
    public class FakturaLookupDto
    {
        public long IdFaktury { get; set; }
        public string NumerFaktury { get; set; } = string.Empty;
        public long IdPodmiotu { get; set; }
        public DateTime DataWystawienia { get; set; }
        public decimal? KwotaBrutto { get; set; }
        public string Label { get; set; } = string.Empty;
    }
}