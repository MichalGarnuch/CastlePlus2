using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Media
{
    public class CreateLicznikRequest
    {
        [Required]
        public long IdPrzylacza { get; set; }

        public long? IdLicznikaNadrzednego { get; set; }

        [Required, StringLength(60)]
        public string NumerNV { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string KodJednostki { get; set; } = string.Empty;

        public decimal? WspolczynnikPrzeliczeniowy { get; set; }

        [Required]
        public bool Aktywny { get; set; }
    }
}
