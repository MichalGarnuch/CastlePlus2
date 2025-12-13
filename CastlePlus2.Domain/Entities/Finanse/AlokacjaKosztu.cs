using System;
using CastlePlus2.Domain.Entities.Finanse;
namespace CastlePlus2.Domain.Entities.Finanse
{
    /// <summary>
    /// Encja 1:1 z tabelą [finanse].[AlokacjaKosztu].
    /// POCO (bez dziedziczenia), bo tabela nie ma pól audytowych.
    /// </summary>
    public class AlokacjaKosztu
    {
        public long IdAlokacji { get; set; }        // PK, IDENTITY(1,1)
        public long IdPozycjiKosztu { get; set; }   // FK -> [finanse].[PozycjaKosztu]
        public Guid IdEncji { get; set; }           // FK -> [rdzen].[Encja]
        public decimal KwotaNetto { get; set; }     // decimal(18,2)
        public decimal KwotaBrutto { get; set; }    // decimal(18,2)

        // Nawigacje opcjonalne (nie tworzą kolumn)
        public PozycjaKosztu? PozycjaKosztu { get; set; }
        public CastlePlus2.Domain.Entities.Rdzen.Encja? Encja { get; set; }
    }
}
