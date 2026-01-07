namespace CastlePlus2.Contracts.DTOs.Dashboard
{
    public class DashboardV1NajemDto
    {
        public int LokaleRazem { get; set; }
        public int LokaleZajete { get; set; }
        public int LokaleWolne { get; set; }

        public List<DashboardV1NajemUmowaDto> UmowyKonczaSieWkrotce { get; set; } = new();
        public List<DashboardV1ZaleglaFakturaDto> ZalegleFaktury { get; set; } = new();
    }
}