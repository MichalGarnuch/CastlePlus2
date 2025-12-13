using System;
using CastlePlus2.Domain.Entities.Podmioty;
using CastlePlus2.Domain.Entities.Slowniki;

namespace CastlePlus2.Domain.Entities.Finanse
{
    /// <summary>
    /// Encja 1:1 z tabelą [finanse].[Faktura].
    /// POCO (bez dziedziczenia), bo tabela nie ma pól audytowych.
    /// </summary>
    public class Faktura
    {
        public long IdFaktury { get; set; }                    // PK, IDENTITY(1,1)
        public string NumerFaktury { get; set; } = string.Empty; // nvarchar(60) NOT NULL (unikalny)
        public long IdPodmiotu { get; set; }                   // FK -> [podmioty].[Podmiot]
        public DateTime DataWystawienia { get; set; }          // date NOT NULL
        public DateTime? DataSprzedazy { get; set; }           // date NULL
        public string KodWaluty { get; set; } = string.Empty;  // char(3) NOT NULL, FK -> [slowniki].[Waluta]
        public decimal? KwotaNetto { get; set; }               // decimal(18,2) NULL
        public decimal? KwotaBrutto { get; set; }              // decimal(18,2) NULL

        // Nawigacje (opcjonalne)
        public Podmiot? Podmiot { get; set; }
        public Waluta? Waluta { get; set; }
    }
}
