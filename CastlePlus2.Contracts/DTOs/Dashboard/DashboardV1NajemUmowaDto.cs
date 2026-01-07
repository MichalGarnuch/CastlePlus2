namespace CastlePlus2.Contracts.DTOs.Dashboard
{
    public class DashboardV1NajemUmowaDto
    {
        public Guid IdUmowy { get; set; }
        public DateOnly? DataZakonczenia { get; set; }
        public long IdNajemcy { get; set; }
        public long IdWynajmujacego { get; set; }
        public string PrzedmiotNajmu { get; set; } = string.Empty;
    }
}