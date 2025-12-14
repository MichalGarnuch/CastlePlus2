using System;

namespace CastlePlus2.Contracts.DTOs.Media
{
    public class OdczytDto
    {
        public long IdOdczytu { get; set; }
        public long IdLicznika { get; set; }
        public DateTime DataOdczytu { get; set; }
        public decimal Wskazanie { get; set; }
        public string? Zrodlo { get; set; }
    }
}
