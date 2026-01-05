using System;
using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.ProcesyPlatnosci.Commands.ZarejestrujPlatnosc
{
    public class ZarejestrujPlatnoscCommand : IRequest<ZarejestrujPlatnoscResultDto>
    {
        public long IdPodmiotu { get; set; }
        public DateTime DataPlatnosci { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public decimal Kwota { get; set; }
        public List<ZarejestrujPlatnoscRozliczenieCommand> Rozliczenia { get; set; } = new();
    }

    public class ZarejestrujPlatnoscRozliczenieCommand
    {
        public long IdFaktury { get; set; }
        public decimal Kwota { get; set; }
    }
}