using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Utrzymanie
{
    public class CreateZleceniePracyRequest
    {
        [Required]
        public Guid IdEncjiGospodarza { get; set; }

        [Required, MaxLength(200)]
        public string Tytul { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Opis { get; set; }

        [Required, MaxLength(20)]
        public string Status { get; set; } = string.Empty;

        public DateTime? DataZamkniecia { get; set; }
    }
}