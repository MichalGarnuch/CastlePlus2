using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Media
{
    public class CreateOdczytRequest
    {
        [Required]
        public long IdLicznika { get; set; }

        [Required]
        public DateTime DataOdczytu { get; set; }

        [Required]
        public decimal Wskazanie { get; set; }

        [StringLength(20)]
        public string? Zrodlo { get; set; }
    }
}
