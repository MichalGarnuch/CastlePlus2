namespace CastlePlus2.Domain.Entities.Auth
{
    /// <summary>
    /// Encja domenowa dla tabeli [auth].[UzytkownikRola].
    /// </summary>
    public class UzytkownikRola
    {
        public int IdUzytkownika { get; set; }
        public int IdRoli { get; set; }

        public virtual Uzytkownik Uzytkownik { get; set; } = null!;
        public virtual Rola Rola { get; set; } = null!;
    }
}