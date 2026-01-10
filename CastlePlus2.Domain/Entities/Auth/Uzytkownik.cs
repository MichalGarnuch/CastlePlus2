using System;
using System.Collections.Generic;

namespace CastlePlus2.Domain.Entities.Auth
{
    /// <summary>
    /// Encja domenowa dla tabeli [auth].[Uzytkownik].
    /// </summary>
    public class Uzytkownik
    {
        public int IdUzytkownika { get; set; }
        public string Login { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string HasloHash { get; set; } = string.Empty;
        public bool CzyAktywny { get; set; }
        public DateTime DataUtworzeniaUtc { get; set; }
        public DateTime DataModyfikacjiUtc { get; set; }
        public DateTime? OstatnieLogowanieUtc { get; set; }

        public virtual ICollection<UzytkownikRola> UzytkownikRole { get; set; } = new List<UzytkownikRola>();
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}