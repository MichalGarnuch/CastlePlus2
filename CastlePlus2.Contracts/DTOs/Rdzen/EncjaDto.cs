using System;

namespace CastlePlus2.Contracts.DTOs.Rdzen
{
    /// <summary>
    /// DTO dla encji Encja – podstawowe dane z rdzenia.
    /// </summary>
    public class EncjaDto
    {
        public Guid Id { get; set; }
        public string TypEncji { get; set; } = string.Empty;
        public string? KodEncji { get; set; }
        public DateTime UtworzonoUtc { get; set; }
        public DateTime ZmienionoUtc { get; set; }
    }
}