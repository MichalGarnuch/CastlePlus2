using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Najem
{
    public class CreateKaucjaRequest
    {
        [Required]
        public Guid IdUmowyNajmu { get; set; }

        [Required, MaxLength(20)]
        public string RodzajOperacji { get; set; } = string.Empty;

        [Required]
        public decimal Kwota { get; set; }

        [Required, StringLength(3, MinimumLength = 3)]
        public string KodWaluty { get; set; } = string.Empty;

        [Required]
        public DateOnly DataOperacji { get; set; }
    }
}