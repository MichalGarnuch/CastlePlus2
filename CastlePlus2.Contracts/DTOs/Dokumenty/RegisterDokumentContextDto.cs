using System;
using System.Collections.Generic;

namespace CastlePlus2.Contracts.DTOs.Dokumenty
{
    public class RegisterDokumentContextDto
    {
        public List<EncjaLookupDto> EncjeLookup { get; set; } = new();
    }

    public class EncjaLookupDto
    {
        public Guid IdEncji { get; set; }
        public string TypEncji { get; set; } = string.Empty;
        public string? KodEncji { get; set; }
        public string Label { get; set; } = string.Empty;
    }
}