using System;

namespace CastlePlus2.Contracts.DTOs.Utrzymanie
{
    public class ZleceniePracyDto
    {
        public long IdZlecenia { get; set; }

        public Guid IdEncjiGospodarza { get; set; }

        public string Tytul { get; set; } = string.Empty;

        public string? Opis { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime DataUtworzenia { get; set; }

        public DateTime? DataZamkniecia { get; set; }
    }
}
