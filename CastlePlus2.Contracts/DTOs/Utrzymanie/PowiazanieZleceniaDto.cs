using System;

namespace CastlePlus2.Contracts.DTOs.Utrzymanie
{
    public class PowiazanieZleceniaDto
    {
        public long IdPowiazania { get; set; }
        public long IdZlecenia { get; set; }
        public Guid IdEncji { get; set; }
    }
}
