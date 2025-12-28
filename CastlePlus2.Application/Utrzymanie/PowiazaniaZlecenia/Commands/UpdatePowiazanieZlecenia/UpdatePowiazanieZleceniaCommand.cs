using System;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.UpdatePowiazanieZlecenia
{
    // Standard: Command płaski, Controller mapuje Request -> Command
    public sealed class UpdatePowiazanieZleceniaCommand : IRequest<bool>
    {
        public long IdPowiazania { get; set; }
        public long IdZlecenia { get; set; }
        public Guid IdEncji { get; set; }
    }
}