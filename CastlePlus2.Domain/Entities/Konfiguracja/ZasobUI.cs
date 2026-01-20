using System;
using System.Collections.Generic;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Domain.Entities.Konfiguracja
{
    public class ZasobUI
    {
        public Guid IdEncji { get; set; }

        public string KodZasobu { get; set; } = null!;
        public string Typ { get; set; } = null!;
        public string? Kategoria { get; set; }

        public bool CzyAktywny { get; set; }
        public int Sort { get; set; }

        public DateTime? WazneOdUtc { get; set; }
        public DateTime? WazneDoUtc { get; set; }

        public DateTime UtworzonoUtc { get; set; }
        public DateTime? ZmienionoUtc { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public virtual Encja Encja { get; set; } = null!;
        public virtual ICollection<ZasobUITekst> Teksty { get; set; } = new List<ZasobUITekst>();
    }
}
