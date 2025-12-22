using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Rdzen
{
    public class UpdatePomieszczenieRequest
    {
        [Required]
        public Guid IdEncjiNadrzednej { get; set; }

        [Required]
        [MaxLength(50)]
        public string KodPomieszczenia { get; set; } = string.Empty;

        public decimal? Powierzchnia { get; set; }
    }
}

