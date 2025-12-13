using System;

namespace CastlePlus2.Domain.Entities.Dokumenty
{
    /// <summary>
    /// Encja domenowa odwzorowująca tabelę [dokumenty].[Dokument].
    /// Uwaga: to NIE jest Encja TPT (rdzen.Encja) – tutaj PK to bigint IDENTITY.
    /// </summary>
    public class Dokument
    {
        public long IdDokumentu { get; set; }              // PK (IDENTITY)
        public Guid? IdEncjiOwner { get; set; }            // FK -> [rdzen].[Encja].[IdEncji] (NULL)
        public string Nazwa { get; set; } = string.Empty;  // NOT NULL (nvarchar(200))
        public string? Opis { get; set; }                  // NULL (nvarchar(1000))
        public string SciezkaPliku { get; set; } = string.Empty; // NOT NULL (nvarchar(400))

        /// <summary>
        /// NOT NULL, domyślnie ustawiane w SQL: sysutcdatetime().
        /// EF ustawimy jako ValueGeneratedOnAdd, żeby nie wymagać wartości na wejściu.
        /// </summary>
        public DateTime DataUtworzenia { get; set; }        // datetime2(0)
    }
}
