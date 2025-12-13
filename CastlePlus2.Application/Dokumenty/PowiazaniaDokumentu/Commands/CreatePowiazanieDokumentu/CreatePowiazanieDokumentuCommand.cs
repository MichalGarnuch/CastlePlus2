using System;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Commands.CreatePowiazanieDokumentu
{
    /// <summary>
    /// Tworzy rekord w [dokumenty].[PowiazanieDokumentu].
    /// Wzorzec PŁASKI (bez CreateDto) – spójny z poprzednimi modułami.
    /// </summary>
    public class CreatePowiazanieDokumentuCommand : IRequest<PowiazanieDokumentuDto>
    {
        public long IdDokumentu { get; set; }
        public Guid IdEncji { get; set; }
    }
}
