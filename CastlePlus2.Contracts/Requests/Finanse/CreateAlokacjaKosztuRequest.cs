using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Finanse
{
    public class CreateAlokacjaKosztuRequest
    {
        [Required]
        public long IdPozycjiKosztu { get; set; }

        [Required]
        public Guid IdEncji { get; set; }

        [Required]
        public decimal KwotaNetto { get; set; }

        [Required]
        public decimal KwotaBrutto { get; set; }
    }
}
