using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Dokumenty
{
    public class RegisterDokumentRequest
    {
        [Required]
        public Guid IdEncji { get; set; }

        [Required, MaxLength(200)]
        public string Nazwa { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Opis { get; set; }

        [MaxLength(400)]
        public string? SciezkaPliku { get; set; }
    }
}