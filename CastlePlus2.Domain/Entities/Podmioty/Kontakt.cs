namespace CastlePlus2.Domain.Entities.Podmioty
{
    /// <summary>
    /// Encja 1:1 z tabelą [podmioty].[Kontakt].
    /// </summary>
    public class Kontakt
    {
        public long IdKontaktu { get; set; }          // PK bigint IDENTITY
        public long IdPodmiotu { get; set; }          // FK -> podmioty.Podmiot(IdPodmiotu)
        public string Rodzaj { get; set; } = default!; // nvarchar(30) NOT NULL
        public string Wartosc { get; set; } = default!; // nvarchar(200) NOT NULL
    }
}
