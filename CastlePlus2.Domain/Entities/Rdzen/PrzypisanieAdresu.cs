using System;

namespace CastlePlus2.Domain.Entities.Rdzen
{
    /// <summary>
    /// [rdzen].[PrzypisanieAdresu]
    /// Uwaga: tabela NIE ma kolumny RowVersion, więc nie dziedziczymy po LongEntity.
    /// </summary>
    public class PrzypisanieAdresu
    {
        public long IdPrzypisaniaAdresu { get; set; }

        public long IdAdresu { get; set; }
        public Guid IdEncji { get; set; }

        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }

        public Adres? Adres { get; set; }
        public Encja? Encja { get; set; }
    }
}
