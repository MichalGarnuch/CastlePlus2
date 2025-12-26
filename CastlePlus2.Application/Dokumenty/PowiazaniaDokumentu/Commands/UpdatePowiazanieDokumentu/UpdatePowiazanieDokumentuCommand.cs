using System;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Commands.UpdatePowiazanieDokumentu
{
    public class UpdatePowiazanieDokumentuCommand : IRequest<bool>
    {
        public long IdPowiazania { get; set; }
        public long IdDokumentu { get; set; }
        public Guid IdEncji { get; set; }
    }
}
