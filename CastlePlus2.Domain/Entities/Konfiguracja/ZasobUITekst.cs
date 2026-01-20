using System;

namespace CastlePlus2.Domain.Entities.Konfiguracja
{
    public class ZasobUITekst
    {
        public long IdZasobuTekstu { get; set; }

        public Guid IdEncji { get; set; }
        public string Jezyk { get; set; } = null!;
        public string Pole { get; set; } = null!;
        public string Wartosc { get; set; } = null!;
        public string Format { get; set; } = null!;
        public int Sort { get; set; }

        public DateTime UtworzonoUtc { get; set; }
        public DateTime? ZmienionoUtc { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public virtual ZasobUI ZasobUI { get; set; } = null!;
    }
}
