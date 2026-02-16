namespace CastlePlus2.Contracts.DTOs.Finanse
{
    public class FakturaWydrukTemplateDto
    {
        public long IdDokumentu { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string? Opis { get; set; }
        public string SciezkaPliku { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
}