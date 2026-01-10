using System.Collections.Generic;

namespace CastlePlus2.Domain.Entities.Auth
{
    /// <summary>
    /// Encja domenowa dla tabeli [auth].[Rola].
    /// </summary>
    public class Rola
    {
        public int IdRoli { get; set; }
        public string Kod { get; set; } = string.Empty;
        public string Nazwa { get; set; } = string.Empty;
        public string? Opis { get; set; }

        public virtual ICollection<UzytkownikRola> UzytkownikRole { get; set; } = new List<UzytkownikRola>();
    }
}