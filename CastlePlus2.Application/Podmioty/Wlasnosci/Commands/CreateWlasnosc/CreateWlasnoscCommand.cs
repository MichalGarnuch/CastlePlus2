using System;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Commands.CreateWlasnosc
{
    public class CreateWlasnoscCommand : IRequest<WlasnoscDto>
    {
        public Guid IdEncji { get; set; }
        public long IdPodmiotu { get; set; }

        public decimal UdzialProcent { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}