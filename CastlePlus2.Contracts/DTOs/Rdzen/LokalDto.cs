using System;

namespace CastlePlus2.Contracts.DTOs.Rdzen
{
    /// <summary>
    /// DTO dla encji Lokal – używane w API.
    /// </summary>
    public class LokalDto
    {
        /// <summary>
        /// IdEncji (PK z rdzen.Encja / rdzen.Lokal).
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// IdEncji budynku, do którego należy lokal.
        /// </summary>
        public Guid IdBudynku { get; set; }

        /// <summary>
        /// Kod lokalu unikalny w obrębie budynku.
        /// </summary>
        public string KodLokalu { get; set; } = string.Empty;

        public decimal? Powierzchnia { get; set; }

        public string? Przeznaczenie { get; set; }
    }
}
