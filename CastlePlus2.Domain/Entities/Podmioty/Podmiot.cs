namespace CastlePlus2.Domain.Entities.Podmioty
{
    /// <summary>
    /// Podmiot (osoba/firma/jednostka). Tabela: [podmioty].[Podmiot]
    /// PK: IdPodmiotu (bigint IDENTITY)
    /// </summary>
    public class Podmiot
    {
        public long IdPodmiotu { get; set; }              // IDENTITY
        public string Nazwa { get; set; } = default!;
        public string? NIP { get; set; }
        public string? REGON { get; set; }
        public string? PESEL { get; set; }
        public string TypPodmiotu { get; set; } = default!;
    }
}
