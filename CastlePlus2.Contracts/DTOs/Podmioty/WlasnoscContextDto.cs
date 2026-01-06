using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Rdzen;

namespace CastlePlus2.Contracts.DTOs.Podmioty
{
    public class WlasnoscContextDto
    {
        public List<EncjaDto> Encje { get; set; } = new();
        public List<PodmiotDto> Podmioty { get; set; } = new();
    }
}