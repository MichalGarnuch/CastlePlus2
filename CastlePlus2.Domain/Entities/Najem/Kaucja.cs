namespace CastlePlus2.Domain.Entities.Najem
{
    /// <summary>
    /// Kaucja do umowy.
    /// Tabela: [najem].[Kaucja]
    /// </summary>
    public class Kaucja
    {
        public long IdOperacjiKaucji { get; set; }
        public Guid IdUmowyNajmu { get; set; }
        public string RodzajOperacji { get; set; } = string.Empty;
        public decimal Kwota { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public DateOnly DataOperacji { get; set; }
    }
}