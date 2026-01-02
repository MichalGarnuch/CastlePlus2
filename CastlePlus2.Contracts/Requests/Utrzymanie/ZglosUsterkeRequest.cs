using System;

namespace CastlePlus2.Contracts.Requests.Utrzymanie
{
    public class ZglosUsterkeRequest
    {
        public Guid IdEncjiGospodarza { get; set; }
        public string Tytul { get; set; } = string.Empty;
        public string? Opis { get; set; }
    }
}