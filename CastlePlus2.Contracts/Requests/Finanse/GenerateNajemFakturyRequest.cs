using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Finanse
{
    public class GenerateNajemFakturyRequest
    {
        [Required]
        [RegularExpression("^\\d{4}-(0[1-9]|1[0-2])$")]
        public string Miesiac { get; set; } = string.Empty;

        [Required]
        public DateTime DataWystawienia { get; set; } = DateTime.Today;
    }
}