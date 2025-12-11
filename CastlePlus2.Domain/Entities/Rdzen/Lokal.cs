using System;
using System.Collections.Generic;
// Usunąłem using System.ComponentModel.DataAnnotations... - w Domenie są zbędne, jeśli mamy FluentAPI

namespace CastlePlus2.Domain.Entities.Rdzen
{
    /// <summary>
    /// Encja domenowa odzwierciedlająca tabelę [rdzen].[Lokal].
    /// Dziedziczy po Encja (Id, TypEncji, RowVersion itd.).
    /// </summary>
    public class Lokal : Encja
    {
        /// <summary>
        /// IdEncji budynku, do którego należy lokal.
        /// </summary>
        public Guid IdBudynku { get; set; }

        /// <summary>
        /// Kod lokalu unikalny w obrębie budynku (UX: IdBudynku + KodLokalu).
        /// Np. "L1-01", "M-2", "U3".
        /// </summary>
        public string KodLokalu { get; set; } = string.Empty;

        /// <summary>
        /// Powierzchnia lokalu w m2.
        /// </summary>
        public decimal? Powierzchnia { get; set; }

        /// <summary>
        /// Przeznaczenie lokalu, np. "mieszkalne", "usługowe".
        /// </summary>
        public string? Przeznaczenie { get; set; }

        // --- NAWIGACJE ---

        /// <summary>
        /// Nawigacja do budynku.
        /// </summary>
        public virtual Budynek Budynek { get; set; } = null!;

        /// <summary>
        /// Kolekcja pomieszczeń w lokalu.
        /// </summary>
        public virtual ICollection<Pomieszczenie> Pomieszczenia { get; set; } = new List<Pomieszczenie>();
    }
}