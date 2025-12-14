using System;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Commands.CreatePrzylacze
{
    public class CreatePrzylaczeCommand : IRequest<PrzylaczeDto>
    {
        public Guid IdEncjiGospodarza { get; set; }
        public string KodRodzaju { get; set; } = string.Empty;
        public string? Opis { get; set; }
    }
}
