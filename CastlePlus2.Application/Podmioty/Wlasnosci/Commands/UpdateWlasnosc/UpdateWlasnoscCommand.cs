using System;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Commands.UpdateWlasnosc
{
    // Standard: Command płaski, Controller mapuje Request -> Command
    public sealed class UpdateWlasnoscCommand : IRequest<bool>
    {
        public long IdWlasnosci { get; set; }

        public Guid IdEncji { get; set; }
        public long IdPodmiotu { get; set; }
        public decimal UdzialProcent { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}