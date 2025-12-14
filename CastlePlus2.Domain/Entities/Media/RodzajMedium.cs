using System;

namespace CastlePlus2.Domain.Entities.Media
{
    /// <summary>
    /// Encja 1:1 z tabelą [media].[RodzajMedium].
    /// Słownik (PK tekstowy), bez pól audytowych.
    /// </summary>
    public class RodzajMedium
    {
        public string KodRodzaju { get; set; } = string.Empty; // PK nvarchar(20)
        public string Nazwa { get; set; } = string.Empty;      // nvarchar(100) NOT NULL
    }
}
