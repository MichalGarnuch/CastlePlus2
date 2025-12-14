namespace CastlePlus2.Domain.Entities.Najem
{
    /// <summary>
    /// Przedmiot najmu (np. Lokal/Budynek/inna Encja).
    /// Tabela: [najem].[PrzedmiotNajmu]
    /// </summary>
    public class PrzedmiotNajmu
    {
        public long IdPrzedmiotuNajmu { get; set; }
        public Guid IdUmowyNajmu { get; set; }
        public Guid IdEncji { get; set; }
        public decimal? UdzialProcent { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}
