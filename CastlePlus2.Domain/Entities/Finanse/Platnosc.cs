using System;
//using CastlePlus2.Domain.Entities.Podmioty;
//using CastlePlus2.Domain.Entities.Slowniki;

namespace CastlePlus2.Domain.Entities.Finanse
{
    /// <summary>
    /// Encja 1:1 z tabelą [finanse].[Platnosc].
    /// POCO (bez dziedziczenia) – tabela nie ma audytu.
    /// </summary>
    public class Platnosc
    {
        public long IdPlatnosci { get; set; }            // PK, IDENTITY(1,1)
        public long IdPodmiotu { get; set; }             // FK -> [podmioty].[Podmiot]
        public DateTime DataPlatnosci { get; set; }      // date NOT NULL
        public string KodWaluty { get; set; } = string.Empty; // char(3) NOT NULL, FK -> [slowniki].[Waluta]
        public decimal Kwota { get; set; }               // decimal(18,2) NOT NULL

        // Nawigacje (opcjonalne)
        // nie zapomnieć odkomentować public Podmiot? Podmiot { get; set; }
        // nie zapomnieć odkomentować public Waluta? Waluta { get; set; }
    }
}
