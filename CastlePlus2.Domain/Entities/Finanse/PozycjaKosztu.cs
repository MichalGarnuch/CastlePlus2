namespace CastlePlus2.Domain.Entities.Finanse
{
    /// <summary>
    /// Encja 1:1 z tabelą [finanse].[PozycjaKosztu].
    /// POCO (bez dziedziczenia) – tabela nie ma audytu.
    /// </summary>
    public class PozycjaKosztu
    {
        public long IdPozycjiKosztu { get; set; }     // PK, IDENTITY(1,1)
        public long IdFaktury { get; set; }           // FK -> [finanse].[Faktura]
        public long IdKategoriiKosztu { get; set; }   // FK -> [finanse].[KategoriaKosztu]
        public string? Opis { get; set; }             // nvarchar(200) NULL
        public decimal KwotaNetto { get; set; }       // decimal(18,2) NOT NULL
        public decimal KwotaBrutto { get; set; }      // decimal(18,2) NOT NULL

        // Nawigacje (opcjonalne)
        public Faktura? Faktura { get; set; }
        public KategoriaKosztu? KategoriaKosztu { get; set; }
    }
}
