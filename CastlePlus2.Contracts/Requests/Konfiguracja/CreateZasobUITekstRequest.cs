using System;

namespace CastlePlus2.Contracts.Requests.Konfiguracja
{
    public class CreateZasobUITekstRequest
    {
        public Guid IdEncji { get; set; }
        public string Jezyk { get; set; } = "pl-PL";
        public string Pole { get; set; } = string.Empty;
        public string Wartosc { get; set; } = string.Empty;
        public string? Format { get; set; }
        public int Sort { get; set; }

        public string? RowVersion { get; set; }
    }
}
