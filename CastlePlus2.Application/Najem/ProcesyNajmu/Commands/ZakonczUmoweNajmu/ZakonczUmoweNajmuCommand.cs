using System;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.ProcesyNajmu.Commands.ZakonczUmoweNajmu
{
    public class ZakonczUmoweNajmuCommand : IRequest<ZakonczUmoweNajmuResult>
    {
        public Guid IdUmowyNajmu { get; set; }
        public DateOnly DataZakonczenia { get; set; }
        public decimal? KwotaZwrotuKaucji { get; set; }
    }
}