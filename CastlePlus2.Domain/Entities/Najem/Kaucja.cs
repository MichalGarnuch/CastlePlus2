namespace CastlePlus2.Domain.Entities.Najem
{
    /// <summary>
    /// Kaucja do umowy.
    /// Tabela: [najem].[Kaucja]
    /// </summary>
    public class Kaucja
    {
        public long IdKaucji { get; set; }
        public Guid IdUmowyNajmu { get; set; }
        public decimal Kwota { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public DateOnly DataWplaty { get; set; }
        public DateOnly? DataZwrotu { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
