namespace CastlePlus2.Contracts.DTOs.Najem
{
    public class PrzedmiotNajmuDto
    {
        public long IdPrzedmiotuNajmu { get; set; }
        public Guid IdUmowyNajmu { get; set; }
        public Guid IdEncji { get; set; }
        public decimal? UdzialProcent { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}
