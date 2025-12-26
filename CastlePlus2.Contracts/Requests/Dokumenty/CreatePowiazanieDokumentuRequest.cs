using System;
using System.ComponentModel.DataAnnotations;

namespace CastlePlus2.Contracts.Requests.Dokumenty
{
    public class CreatePowiazanieDokumentuRequest
    {
        [Required]
        public long IdDokumentu { get; set; }

        [Required]
        public Guid IdEncji { get; set; }
    }
}
