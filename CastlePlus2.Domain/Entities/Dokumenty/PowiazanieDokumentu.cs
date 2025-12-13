using System;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Domain.Entities.Dokumenty
{
    /// <summary>
    /// Encja 1:1 z tabelą [dokumenty].[PowiazanieDokumentu].
    /// Celowo POCO (bez klas bazowych), bo tabela nie ma pól audytowych.
    /// </summary>
    public class PowiazanieDokumentu
    {
        public long IdPowiazania { get; set; }   // PK, IDENTITY(1,1)
        public long IdDokumentu { get; set; }    // FK -> [dokumenty].[Dokument]
        public Guid IdEncji { get; set; }        // FK -> [rdzen].[Encja]

        // Nawigacje (opcjonalne, nie tworzą kolumn)
        public Dokument? Dokument { get; set; }
        public Encja? Encja { get; set; }
    }
}
