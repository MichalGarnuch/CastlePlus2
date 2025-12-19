namespace CastlePlus2.Domain.Entities.Slowniki
{
    /// <summary>
    /// Słownik indeksacji (np. CPI, stała, brak).
    /// Tabela: [slowniki].[Indeksacja]
    /// PK: KodIndeksacji (nvarchar(20))
    /// </summary>
    public class Indeksacja
    {
        public string KodIndeksacji { get; set; } = default!;
        public string Nazwa { get; set; } = default!;
        public string? AdresZrodlaURL { get; set; } // nvarchar(400) NULL
    }
}
