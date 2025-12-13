using System;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Commands.CreateDokument
{
    /// <summary>
    /// Komenda tworząca dokument – wzorzec PŁASKI (bez CreateDto).
    /// </summary>
    public class CreateDokumentCommand : IRequest<DokumentDto>
    {
        public Guid? IdEncjiOwner { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string? Opis { get; set; }
        public string SciezkaPliku { get; set; } = string.Empty;
    }
}
