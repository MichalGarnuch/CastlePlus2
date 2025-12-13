namespace CastlePlus2.Domain.Entities.Finanse
{
    /// <summary>
    /// Encja 1:1 z tabelą [finanse].[RozliczeniePlatnosci].
    /// POCO (bez dziedziczenia).
    /// </summary>
    public class RozliczeniePlatnosci
    {
        public long IdRozliczenia { get; set; }   // PK, IDENTITY
        public long IdPlatnosci { get; set; }     // FK -> finanse.Platnosc
        public long IdFaktury { get; set; }       // FK -> finanse.Faktura
        public decimal Kwota { get; set; }        // decimal(18,2)

        // Nawigacje (opcjonalne)
        public Platnosc? Platnosc { get; set; }
        public Faktura? Faktura { get; set; }
    }
}
