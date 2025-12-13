using System;

namespace CastlePlus2.Contracts.DTOs.Finanse
{
    public class PlatnoscDto
    {
        public long IdPlatnosci { get; set; }
        public long IdPodmiotu { get; set; }
        public DateTime DataPlatnosci { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public decimal Kwota { get; set; }
    }
}
