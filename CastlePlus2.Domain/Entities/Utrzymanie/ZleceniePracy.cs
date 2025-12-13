using System;

namespace CastlePlus2.Domain.Entities.Utrzymanie
{
    /// <summary>
    /// Encja 1:1 z tabelą [utrzymanie].[ZleceniePracy].
    /// </summary>
    public class ZleceniePracy
    {
        public long IdZlecenia { get; set; } // IDENTITY(1,1)

        public Guid IdEncjiGospodarza { get; set; }

        public string Tytul { get; set; } = string.Empty;

        public string? Opis { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime DataUtworzenia { get; set; }

        public DateTime? DataZamkniecia { get; set; }
    }
}
