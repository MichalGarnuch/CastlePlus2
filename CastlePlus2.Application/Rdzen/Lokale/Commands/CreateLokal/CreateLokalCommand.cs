using System;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Lokale.Commands.CreateLokal
{
    /// <summary>
    /// Komenda tworząca nowy lokal w istniejącym budynku.
    /// </summary>
    public class CreateLokalCommand : IRequest<LokalDto>
    {
        public Guid IdBudynku { get; set; }

        public string KodLokalu { get; set; } = string.Empty;

        public decimal? Powierzchnia { get; set; }

        public string? Przeznaczenie { get; set; }
    }
}
