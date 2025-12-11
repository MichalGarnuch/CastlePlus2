using System;

namespace CastlePlus2.Contracts.DTOs.Rdzen
{
    /// <summary>
    /// DTO wysyłane na API dla pomieszczeń.
    /// </summary>
    public class PomieszczenieDto
    {
        public Guid Id { get; set; }

        public Guid IdEncjiNadrzednej { get; set; }

        public string KodPomieszczenia { get; set; } = string.Empty;

        public decimal? Powierzchnia { get; set; }
    }
}
