using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Rdzen
{
    public class UpdateAdresRequest
    {
        [MaxLength(60)]
        public string Kraj { get; set; } = "Polska";

        [MaxLength(60)]
        public string? Wojewodztwo { get; set; }

        [MaxLength(60)]
        public string? Powiat { get; set; }

        [MaxLength(60)]
        public string? Gmina { get; set; }

        [Required, MaxLength(100)]
        public string Miejscowosc { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Ulica { get; set; }

        [MaxLength(20)]
        public string? Numer { get; set; }

        [MaxLength(12)]
        public string? KodPocztowy { get; set; }

        [MaxLength(300)]
        public string? AdresPelny { get; set; }
    }
}
