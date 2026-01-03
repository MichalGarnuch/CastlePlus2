using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Finanse
{
    public class WystawFaktureRequest
    {
        [Required, MaxLength(60)]
        public string NumerFaktury { get; set; } = string.Empty;

        [Range(1, long.MaxValue)]
        public long IdPodmiotu { get; set; }

        [Required]
        public DateTime DataWystawienia { get; set; }

        public DateTime? DataSprzedazy { get; set; }

        [Required, StringLength(3, MinimumLength = 3)]
        public string KodWaluty { get; set; } = string.Empty;

        [Required]
        public List<WystawFakturePozycjaRequest> Pozycje { get; set; } = new();
    }

    public class WystawFakturePozycjaRequest
    {
        [Range(1, long.MaxValue)]
        public long IdKategoriiKosztu { get; set; }

        [MaxLength(200)]
        public string? Opis { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal KwotaNetto { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal KwotaBrutto { get; set; }

        [Required]
        public List<WystawFaktureAlokacjaRequest> Alokacje { get; set; } = new();
    }

    public class WystawFaktureAlokacjaRequest
    {
        [Required]
        public Guid IdEncji { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal KwotaNetto { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal KwotaBrutto { get; set; }
    }
}