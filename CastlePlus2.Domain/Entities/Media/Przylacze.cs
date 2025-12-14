using System;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Domain.Entities.Media
{
    /// <summary>
    /// Encja 1:1 z tabelą [media].[Przylacze].
    /// PK bigint (IDENTITY), FK do rdzen.Encja oraz media.RodzajMedium.
    /// </summary>
    public class Przylacze
    {
        public long IdPrzylacza { get; set; } // PK bigint IDENTITY(1,1)
        public Guid IdEncjiGospodarza { get; set; } // FK -> [rdzen].[Encja]
        public string KodRodzaju { get; set; } = string.Empty; // FK -> [media].[RodzajMedium]
        public string? Opis { get; set; } // nvarchar(200) NULL

        // Nawigacje (czytelność domeny, brak logiki)
        public Encja? EncjaGospodarza { get; set; }
        public RodzajMedium? RodzajMedium { get; set; }
    }
}
