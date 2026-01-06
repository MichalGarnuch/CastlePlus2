using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Podmioty
{
    public class UstawWlasnoscRequest
    {
        [Required]
        public Guid IdEncji { get; set; }

        [Required]
        public List<UstawWlasnoscItemRequest> Udzialy { get; set; } = new();
    }

    public class UstawWlasnoscItemRequest
    {
        [Range(1, long.MaxValue)]
        public long IdPodmiotu { get; set; }

        [Range(typeof(decimal), "0.01", "100.00")]
        public decimal UdzialProcent { get; set; }

        [Required]
        public DateOnly OdDnia { get; set; }

        public DateOnly? DoDnia { get; set; }
    }
}