using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Finanse
{
    public class UpdateKategoriaKosztuRequest
    {
        [Required, MaxLength(20)]
        public string Kod { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Nazwa { get; set; } = string.Empty;
    }
}
