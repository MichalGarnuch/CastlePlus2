using System;

namespace CastlePlus2.Contracts.DTOs.Najem
{
    public class UmowaNajmuLookupDto
    {
        public Guid IdUmowy { get; set; }
        public string NumerUmowy { get; set; } = string.Empty;
        public DateTime DataPoczatku { get; set; }
        public DateTime? DataZakonczenia { get; set; }
        public string Label { get; set; } = string.Empty;
    }
}