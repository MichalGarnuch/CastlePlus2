using System;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Commands.UpdateDokument
{
    public class UpdateDokumentCommand : IRequest<bool>
    {
        public long IdDokumentu { get; set; }

        public Guid? IdEncjiOwner { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string? Opis { get; set; }
        public string SciezkaPliku { get; set; } = string.Empty;
    }
}
