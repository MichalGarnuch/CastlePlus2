using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.DTOs.Slowniki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastlePlus2.Contracts.DTOs.Najem
{
    public class UmowaNajmuContextDto
    {
        public List<LokalDto> Lokale { get; set; } = new();
        public List<PodmiotDto> Podmioty { get; set; } = new();
        public List<WalutaDto> Waluty { get; set; } = new();
        public List<IndeksacjaDto> Indeksacje { get; set; } = new();
        public List<JednostkaMiaryDto> JednostkiMiary { get; set; } = new();
    }
}
