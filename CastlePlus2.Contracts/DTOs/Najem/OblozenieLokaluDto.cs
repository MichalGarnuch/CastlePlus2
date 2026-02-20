namespace CastlePlus2.Contracts.DTOs.Najem
{
    public class OblozenieLokaluDto
    {
        public Guid IdNieruchomosci { get; set; }
        public string NazwaNieruchomosci { get; set; } = string.Empty;
        public Guid IdBudynku { get; set; }
        public string KodBudynku { get; set; } = string.Empty;
        public Guid IdLokalu { get; set; }
        public string KodLokalu { get; set; } = string.Empty;
        public bool CzyZajety { get; set; }
        public Guid? IdUmowyNajmu { get; set; }
        public string? NajemcaNazwa { get; set; }
        public DateTime? UmowaOd { get; set; }
        public DateTime? UmowaDo { get; set; }
    }
}