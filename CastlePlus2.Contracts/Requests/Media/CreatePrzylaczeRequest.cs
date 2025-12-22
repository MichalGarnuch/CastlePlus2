using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Media
{
    public class CreatePrzylaczeRequest
    {
        [Required]
        public Guid IdEncjiGospodarza { get; set; }

        [Required, StringLength(20)]
        public string KodRodzaju { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Opis { get; set; }
    }
}
