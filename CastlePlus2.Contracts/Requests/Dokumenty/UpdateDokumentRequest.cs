using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Dokumenty
{
    public class UpdateDokumentRequest
    {
        public Guid? IdEncjiOwner { get; set; }

        [Required, MaxLength(200)]
        public string Nazwa { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Opis { get; set; }

        [Required, MaxLength(400)]
        public string SciezkaPliku { get; set; } = string.Empty;
    }
}
