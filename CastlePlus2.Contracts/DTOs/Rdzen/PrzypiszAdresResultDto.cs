using System;

namespace CastlePlus2.Contracts.DTOs.Rdzen
{
    public class PrzypiszAdresResultDto
    {
        public long IdPrzypisaniaAdresu { get; set; }
        public Guid IdEncji { get; set; }
        public long IdAdresu { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}