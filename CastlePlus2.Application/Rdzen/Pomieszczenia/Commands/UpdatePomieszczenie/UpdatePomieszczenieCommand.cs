using System;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.UpdatePomieszczenie
{
    public class UpdatePomieszczenieCommand : IRequest<PomieszczenieDto?>
    {
        public Guid Id { get; set; }

        public Guid IdEncjiNadrzednej { get; set; }

        public string KodPomieszczenia { get; set; } = string.Empty;

        public decimal? Powierzchnia { get; set; }
    }
}
