using System;
using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Commands.UstawWlasnosc
{
    public class UstawWlasnoscCommand : IRequest<IReadOnlyList<WlasnoscDto>>
    {
        public Guid IdEncji { get; set; }
        public List<UstawWlasnoscUdzialCommand> Udzialy { get; set; } = new();
    }

    public class UstawWlasnoscUdzialCommand
    {
        public long IdPodmiotu { get; set; }
        public decimal UdzialProcent { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}
