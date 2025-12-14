namespace CastlePlus2.Contracts.DTOs.Najem
{
    public class KaucjaDto
    {
        public long IdKaucji { get; set; }
        public Guid IdUmowyNajmu { get; set; }
        public decimal Kwota { get; set; }
        public string KodWaluty { get; set; } = default!;
        public DateOnly DataWplaty { get; set; }
        public DateOnly? DataZwrotu { get; set; }
        public string Status { get; set; } = default!;
    }
}
