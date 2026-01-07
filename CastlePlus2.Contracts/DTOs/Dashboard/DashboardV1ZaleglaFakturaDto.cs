namespace CastlePlus2.Contracts.DTOs.Dashboard
{
    public class DashboardV1ZaleglaFakturaDto
    {
        public long IdFaktury { get; set; }
        public string NumerFaktury { get; set; } = string.Empty;
        public DateOnly DataWystawienia { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public decimal KwotaBrutto { get; set; }
        public decimal KwotaPozostala { get; set; }
    }
}