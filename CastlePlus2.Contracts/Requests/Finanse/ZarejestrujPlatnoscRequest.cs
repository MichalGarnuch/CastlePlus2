using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Finanse
{
    public class ZarejestrujPlatnoscRequest
    {
        [Range(1, long.MaxValue)]
        public long IdPodmiotu { get; set; }

        [Required]
        public DateTime DataPlatnosci { get; set; }

        [Required, StringLength(3, MinimumLength = 3)]
        public string KodWaluty { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Kwota { get; set; }

        [Required]
        public List<ZarejestrujPlatnoscRozliczenieRequest> Rozliczenia { get; set; } = new();
    }

    public class ZarejestrujPlatnoscRozliczenieRequest
    {
        [Range(1, long.MaxValue)]
        public long IdFaktury { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Kwota { get; set; }
    }
}