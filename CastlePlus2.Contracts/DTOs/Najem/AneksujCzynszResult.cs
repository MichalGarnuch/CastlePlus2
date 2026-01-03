using System;

namespace CastlePlus2.Contracts.DTOs.Najem
{
    public class AneksujCzynszResult
    {
        public Guid IdUmowyNajmu { get; set; }
        public long IdSkladnikaCzynszu { get; set; }
        public string? Message { get; set; }
    }
}