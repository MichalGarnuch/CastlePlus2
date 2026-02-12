namespace CastlePlus2.Contracts.DTOs.Media
{
    public class LicznikLookupDto
    {
        public long IdLicznika { get; set; }
        public string NumerNV { get; set; } = string.Empty;
        public string KodJednostki { get; set; } = string.Empty;
        public bool Aktywny { get; set; }
        public string Label { get; set; } = string.Empty;
    }
}