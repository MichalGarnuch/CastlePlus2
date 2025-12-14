using System;

namespace CastlePlus2.Domain.Entities.Podmioty
{
    /// <summary>
    /// Encja 1:1 z tabelą [podmioty].[Wlasnosc].
    /// Łączy Podmiot z Encją (np. Nieruchomosc/Budynek/Lokal) wraz z udziałem w czasie.
    /// </summary>
    public class Wlasnosc
    {
        public long IdWlasnosci { get; set; }                 // PK bigint IDENTITY
        public Guid IdEncji { get; set; }                     // FK -> rdzen.Encja(IdEncji)
        public long IdPodmiotu { get; set; }                  // FK -> podmioty.Podmiot(IdPodmiotu)

        public decimal UdzialProcent { get; set; }            // decimal(7,4) NOT NULL
        public DateOnly OdDnia { get; set; }                  // date NOT NULL
        public DateOnly? DoDnia { get; set; }                 // date NULL

        public byte[] RowVersion { get; set; } = Array.Empty<byte>(); // timestamp (concurrency)
    }
}
