using System;
using System.Collections.Generic;

namespace CastlePlus2.Contracts.DTOs.Utrzymanie
{
    public class ZglosUsterkeContextDto
    {
        public List<LokalLookupDto> LokaleLookup { get; set; } = new();
        public List<BudynekLookupDto> BudynkiLookup { get; set; } = new();
    }

    public class LokalLookupDto
    {
        public Guid IdEncji { get; set; }
        public string KodBudynku { get; set; } = string.Empty;
        public string KodLokalu { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }

    public class BudynekLookupDto
    {
        public Guid IdEncji { get; set; }
        public string KodBudynku { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
}