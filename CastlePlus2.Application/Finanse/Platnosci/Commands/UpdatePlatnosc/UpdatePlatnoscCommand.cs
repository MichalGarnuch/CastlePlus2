using System;
using MediatR;

namespace CastlePlus2.Application.Finanse.Platnosci.Commands.UpdatePlatnosc
{
    // Standard: Command płaski, Controller mapuje Request -> Command
    public sealed class UpdatePlatnoscCommand : IRequest<bool>
    {
        public long IdPlatnosci { get; set; }
        public long IdPodmiotu { get; set; }
        public DateTime DataPlatnosci { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public decimal Kwota { get; set; }
    }
}