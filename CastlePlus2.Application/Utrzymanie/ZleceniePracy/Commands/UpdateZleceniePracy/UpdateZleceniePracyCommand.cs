using System;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.UpdateZleceniePracy
{
    public class UpdateZleceniePracyCommand : IRequest<ZleceniePracyDto?>
    {
        public long IdZlecenia { get; set; }

        public Guid IdEncjiGospodarza { get; set; }

        public string Tytul { get; set; } = string.Empty;

        public string? Opis { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime? DataZamkniecia { get; set; }
    }
}