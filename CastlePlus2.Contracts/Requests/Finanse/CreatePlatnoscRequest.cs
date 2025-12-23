using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Finanse
{
    public class CreatePlatnoscRequest
    {
        [Required]
        public long IdPodmiotu { get; set; }

        [Required]
        public DateTime DataPlatnosci { get; set; }

        [Required, StringLength(3)]
        public string KodWaluty { get; set; } = string.Empty;

        [Required]
        public decimal Kwota { get; set; }
    }
}
