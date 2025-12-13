using System;

namespace CastlePlus2.Contracts.DTOs.Dokumenty
{
    public class PowiazanieDokumentuDto
    {
        public long IdPowiazania { get; set; }
        public long IdDokumentu { get; set; }
        public Guid IdEncji { get; set; }
    }
}
