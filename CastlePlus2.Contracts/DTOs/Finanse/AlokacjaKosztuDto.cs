using System;

namespace CastlePlus2.Contracts.DTOs.Finanse
{
    public class AlokacjaKosztuDto
    {
        public long IdAlokacji { get; set; }
        public long IdPozycjiKosztu { get; set; }
        public Guid IdEncji { get; set; }
        public decimal KwotaNetto { get; set; }
        public decimal KwotaBrutto { get; set; }
    }
}
