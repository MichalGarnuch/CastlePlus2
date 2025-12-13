namespace CastlePlus2.Domain.Entities.Finanse
{
    /// <summary>
    /// Encja 1:1 z tabelą [finanse].[KategoriaKosztu].
    /// POCO (bez dziedziczenia), bo tabela nie ma pól audytowych.
    /// </summary>
    public class KategoriaKosztu
    {
        public long IdKategoriiKosztu { get; set; }   // PK, IDENTITY(1,1)
        public string Kod { get; set; } = string.Empty;   // nvarchar(20) NOT NULL (unikalny)
        public string Nazwa { get; set; } = string.Empty; // nvarchar(100) NOT NULL
    }
}
