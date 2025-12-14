namespace CastlePlus2.Contracts.DTOs.Najem
{
    public class UmowaNajmuDto
    {
        public Guid Id { get; set; }
        public long IdNajemcy { get; set; }
        public long IdWynajmujacego { get; set; }
        public string NumerUmowy { get; set; } = default!;
        public DateOnly DataOd { get; set; }
        public DateOnly? DataDo { get; set; }
        public string Status { get; set; } = default!;
        public string KodWaluty { get; set; } = default!;
        public string? KodIndeksacji { get; set; }
    }
}
