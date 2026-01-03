using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Najem
{
    public class ZakonczUmoweNajmuRequest
    {
        [Required]
        public DateOnly DataZakonczenia { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal? KwotaZwrotuKaucji { get; set; }
    }
}