namespace CastlePlus2.Contracts.DTOs.Dashboard
{
    public class NajemDashboardDto
    {
        public int LokaleRazem { get; set; }
        public int LokaleZajete { get; set; }
        public int LokaleWolne { get; set; }
        public List<WygasajacaUmowaDto> WygasajaceUmowy { get; set; } = new();
    }
}