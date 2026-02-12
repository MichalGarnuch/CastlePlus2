using System;

namespace CastlePlus2.Contracts.DTOs.Media
{
    public class PrzylaczeLookupDto
    {
        public long IdPrzylacza { get; set; }
        public Guid IdEncjiGospodarza { get; set; }
        public string KodRodzaju { get; set; } = string.Empty;
        public string? Opis { get; set; }
        public string Label { get; set; } = string.Empty;
    }
}