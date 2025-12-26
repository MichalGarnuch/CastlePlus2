using System;

namespace CastlePlus2.Contracts.Requests.Utrzymanie
{
    public sealed class CreatePowiazanieZleceniaRequest
    {
        public long IdZlecenia { get; set; }
        public Guid IdEncji { get; set; }
    }
}