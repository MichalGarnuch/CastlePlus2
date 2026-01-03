using System;
using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.DTOs.Slowniki;

namespace CastlePlus2.Contracts.DTOs.Finanse
{
    public class WystawFaktureContextDto
    {
        public List<PodmiotDto> Podmioty { get; set; } = new();
        public List<WalutaDto> Waluty { get; set; } = new();
        public List<KategoriaKosztuDto> KategorieKosztow { get; set; } = new();
        public List<EncjaLookupDto> Encje { get; set; } = new();
    }

    public class EncjaLookupDto
    {
        public Guid IdEncji { get; set; }
        public string TypEncji { get; set; } = string.Empty;
        public string? KodEncji { get; set; }
        public string Label { get; set; } = string.Empty;
    }
}
