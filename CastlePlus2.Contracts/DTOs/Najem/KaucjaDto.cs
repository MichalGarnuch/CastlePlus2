namespace CastlePlus2.Contracts.DTOs.Najem
{
    public class KaucjaDto
    {
        public long IdOperacjiKaucji { get; set; }
        public Guid IdUmowyNajmu { get; set; }
        public string RodzajOperacji { get; set; } = default!;
        public decimal Kwota { get; set; }
        public string KodWaluty { get; set; } = default!;
        public DateOnly DataOperacji { get; set; }
    }
}