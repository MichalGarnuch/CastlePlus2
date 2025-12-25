namespace CastlePlus2.Contracts.DTOs.Najem
{
    public class UmowaNajmuDto
    {
        public Guid Id { get; set; }
        public string NumerUmowy { get; set; } = string.Empty;
        public long IdWynajmujacego { get; set; }
        public long IdNajemcy { get; set; }
        public DateTime DataZawarcia { get; set; }
        public DateTime DataPoczatku { get; set; }
        public DateTime? DataZakonczenia { get; set; }
        public string KodWaluty { get; set; } = default!;
        public string? KodIndeksacji { get; set; }
    }
}