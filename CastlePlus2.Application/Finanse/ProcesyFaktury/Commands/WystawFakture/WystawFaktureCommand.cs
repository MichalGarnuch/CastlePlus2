using System;
using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.WystawFakture
{
    public class WystawFaktureCommand : IRequest<WystawFaktureResultDto>
    {
        public string NumerFaktury { get; set; } = string.Empty;
        public long IdPodmiotu { get; set; }
        public DateTime DataWystawienia { get; set; }
        public DateTime? DataSprzedazy { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public List<WystawFakturePozycjaCommand> Pozycje { get; set; } = new();
    }

    public class WystawFakturePozycjaCommand
    {
        public long IdKategoriiKosztu { get; set; }
        public string? Opis { get; set; }
        public decimal KwotaNetto { get; set; }
        public decimal KwotaBrutto { get; set; }
        public List<WystawFaktureAlokacjaCommand> Alokacje { get; set; } = new();
    }

    public class WystawFaktureAlokacjaCommand
    {
        public Guid IdEncji { get; set; }
        public decimal KwotaNetto { get; set; }
        public decimal KwotaBrutto { get; set; }
    }
}