using System;

namespace CastlePlus2.Contracts.DTOs.Najem
{
    public class ZakonczUmoweNajmuResult
    {
        public Guid IdUmowyNajmu { get; set; }
        public DateOnly DataZakonczenia { get; set; }
        public long? IdOperacjiKaucji { get; set; }
        public string? Message { get; set; }
    }
}