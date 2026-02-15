using System;
using System.Collections.Generic;

namespace CastlePlus2.Contracts.DTOs.Finanse
{
    public class GenerateNajemFakturyResultDto
    {
        public string Miesiac { get; set; } = string.Empty;
        public DateTime DataWystawienia { get; set; }
        public List<GenerateNajemFakturyItemDto> Items { get; set; } = new();
    }

    public class GenerateNajemFakturyItemDto
    {
        public Guid IdUmowyNajmu { get; set; }
        public long IdNajemcy { get; set; }
        public string NumerFaktury { get; set; } = string.Empty;
        public decimal KwotaNetto { get; set; }
        public decimal KwotaBrutto { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<string> Warnings { get; set; } = new();
        public string? Error { get; set; }
    }
}
