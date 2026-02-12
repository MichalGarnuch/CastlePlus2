using System;

namespace CastlePlus2.Contracts.DTOs.Najem
{
    public class PrzedmiotNajmuLookupDto
    {
        public long IdPrzedmiotuNajmu { get; set; }
        public Guid IdUmowyNajmu { get; set; }
        public Guid IdEncji { get; set; }
        public string EncjaLabel { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
}