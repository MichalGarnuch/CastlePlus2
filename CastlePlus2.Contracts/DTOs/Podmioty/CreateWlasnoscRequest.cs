using System;

namespace CastlePlus2.Contracts.DTOs.Podmioty
{
    public class CreateWlasnoscRequest
    {
        public Guid IdEncji { get; set; }
        public long IdPodmiotu { get; set; }

        public decimal UdzialProcent { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}
