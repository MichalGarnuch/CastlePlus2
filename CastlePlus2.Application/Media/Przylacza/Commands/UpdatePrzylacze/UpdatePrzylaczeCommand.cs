using System;
using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Commands.UpdatePrzylacze
{
    // Standard: Command płaski, Controller mapuje Request -> Command
    public sealed class UpdatePrzylaczeCommand : IRequest<bool>
    {
        public long IdPrzylacza { get; set; }

        public Guid IdEncjiGospodarza { get; set; }
        public string KodRodzaju { get; set; } = string.Empty;
        public string? Opis { get; set; }
    }
}