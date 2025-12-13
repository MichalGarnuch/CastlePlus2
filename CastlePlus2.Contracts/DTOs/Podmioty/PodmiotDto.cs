namespace CastlePlus2.Contracts.DTOs.Podmioty
{
    public class PodmiotDto
    {
        public long IdPodmiotu { get; set; }
        public string Nazwa { get; set; } = default!;
        public string? NIP { get; set; }
        public string? REGON { get; set; }
        public string? PESEL { get; set; }
        public string TypPodmiotu { get; set; } = default!;
    }
}
