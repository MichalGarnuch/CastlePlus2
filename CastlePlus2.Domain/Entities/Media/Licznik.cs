using System;

namespace CastlePlus2.Domain.Entities.Media
{
    /// <summary>
    /// Encja 1:1 z tabelą [media].[Licznik].
    /// Uwaga: bigint IDENTITY w SQL => long w C#.
    /// </summary>
    public class Licznik
    {
        public long IdLicznika { get; set; } // PK bigint IDENTITY(1,1)
        public long IdPrzylacza { get; set; } // FK -> media.Przylacze(IdPrzylacza)
        public long? IdLicznikaNadrzednego { get; set; } // FK -> media.Licznik(IdLicznika) NULL

        public string NumerNV { get; set; } = string.Empty; // nvarchar(60) NOT NULL (UNIQUE)
        public string KodJednostki { get; set; } = string.Empty; // nvarchar(20) NOT NULL (FK -> slowniki.JednostkaMiary)

        public decimal? WspolczynnikPrzeliczeniowy { get; set; } // decimal(12,6) NULL
        public bool Aktywny { get; set; } // bit NOT NULL

        // Nawigacje (wygodne w EF, nie psują zgodności 1:1 z kolumnami)
        public Przylacze? Przylacze { get; set; }
        public Licznik? LicznikNadrzedny { get; set; }
    }
}
