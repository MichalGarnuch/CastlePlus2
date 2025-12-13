using System;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.CreateZleceniePracy
{
    /// <summary>
    /// Komenda tworząca nowe zlecenie pracy.
    /// </summary>
    public class CreateZleceniePracyCommand : IRequest<ZleceniePracyDto>
    {
        public Guid IdEncjiGospodarza { get; set; }

        public string Tytul { get; set; } = string.Empty;

        public string? Opis { get; set; }

        /// <summary>
        /// Status tekstowy (nvarchar(20)). Trzymamy prosto jak w SQL.
        /// </summary>
        public string Status { get; set; } = "Nowe";
    }
}
