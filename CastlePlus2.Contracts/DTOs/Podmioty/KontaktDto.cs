namespace CastlePlus2.Contracts.DTOs.Podmioty
{
    public class KontaktDto
    {
        public long IdKontaktu { get; set; }
        public long IdPodmiotu { get; set; }
        public string Rodzaj { get; set; } = default!;
        public string Wartosc { get; set; } = default!;
    }
}
