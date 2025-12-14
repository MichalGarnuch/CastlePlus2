using System;

namespace CastlePlus2.Domain.Entities.Media
{
    /// <summary>
    /// Encja 1:1 z tabelą [media].[Odczyt].
    /// </summary>
    public class Odczyt
    {
        public long IdOdczytu { get; set; }      // PK bigint IDENTITY
        public long IdLicznika { get; set; }     // FK -> media.Licznik(IdLicznika)
        public DateTime DataOdczytu { get; set; } // SQL: date (w .NET trzymamy DateTime, bez czasu)
        public decimal Wskazanie { get; set; }   // decimal(18,6) NOT NULL
        public string? Zrodlo { get; set; }      // nvarchar(20) NULL
    }
}
