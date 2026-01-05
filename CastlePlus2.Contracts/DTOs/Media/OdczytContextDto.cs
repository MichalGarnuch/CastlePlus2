using System.Collections.Generic;

namespace CastlePlus2.Contracts.DTOs.Media
{
    public class OdczytContextDto
    {
        public List<LicznikOdczytLookupDto> Liczniki { get; set; } = new();
    }

    public class LicznikOdczytLookupDto
    {
        public long IdLicznika { get; set; }
        public string NumerNV { get; set; } = string.Empty;
        public string KodJednostki { get; set; } = string.Empty;
    }
}