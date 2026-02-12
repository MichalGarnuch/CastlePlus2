namespace CastlePlus2.Contracts.DTOs.Podmioty
{
    public class PodmiotLookupDto
    {
        public long IdPodmiotu { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string? NIP { get; set; }
        public string? REGON { get; set; }
        public string? PESEL { get; set; }
        public string TypPodmiotu { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
}