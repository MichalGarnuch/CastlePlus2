using System;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Domain.Entities.Utrzymanie
{
    /// <summary>
    /// Encja 1:1 z tabelą [utrzymanie].[PowiazanieZlecenia].
    /// Celowo POCO (bez bazowej klasy), żeby nie mapować kolumn spoza SQL.
    /// </summary>
    public class PowiazanieZlecenia
    {
        public long IdPowiazania { get; set; }   // PK, IDENTITY(1,1)
        public long IdZlecenia { get; set; }     // FK -> [utrzymanie].[ZleceniePracy]
        public Guid IdEncji { get; set; }        // FK -> [rdzen].[Encja]

        // Nawigacje (nie tworzą kolumn w SQL)
        public ZleceniePracy? ZleceniePracy { get; set; }
        public Encja? Encja { get; set; }
    }
}
