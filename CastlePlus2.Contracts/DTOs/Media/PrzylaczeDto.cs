using System;

namespace CastlePlus2.Contracts.DTOs.Media
{
    public class PrzylaczeDto
    {
        public long IdPrzylacza { get; set; }
        public Guid IdEncjiGospodarza { get; set; }
        public string KodRodzaju { get; set; } = string.Empty;
        public string? Opis { get; set; }

        // Wygodne na odczyt (bez JOINów w kontrolerze)
        public string? NazwaRodzaju { get; set; }
    }
}
