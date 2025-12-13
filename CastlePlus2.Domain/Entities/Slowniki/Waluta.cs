namespace CastlePlus2.Domain.Entities.Slowniki
{
    /// <summary>
    /// Słownik walut. Tabela: [slowniki].[Waluta]
    /// PK: KodWaluty (char(3))
    /// </summary>
    public class Waluta
    {
        public string KodWaluty { get; set; } = default!;
        public string Nazwa { get; set; } = default!;
    }
}
