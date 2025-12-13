using System;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Platnosci.Commands.CreatePlatnosc
{
    public class CreatePlatnoscCommand : IRequest<PlatnoscDto>
    {
        public long IdPodmiotu { get; set; }
        public DateTime DataPlatnosci { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public decimal Kwota { get; set; }
    }
}
