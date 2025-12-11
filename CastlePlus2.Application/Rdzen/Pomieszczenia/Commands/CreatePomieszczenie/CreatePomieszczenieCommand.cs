using System;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.CreatePomieszczenie
{
    /// <summary>
    /// Komenda tworząca nowe pomieszczenie w istniejącym lokalu.
    /// </summary>
    public class CreatePomieszczenieCommand : IRequest<PomieszczenieDto>
    {
        public Guid IdEncjiNadrzednej { get; set; }

        public string KodPomieszczenia { get; set; } = string.Empty;

        public decimal? Powierzchnia { get; set; }
    }
}
