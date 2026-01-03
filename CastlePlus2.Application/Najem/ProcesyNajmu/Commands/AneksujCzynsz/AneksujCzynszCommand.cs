using System;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.ProcesyNajmu.Commands.AneksujCzynsz
{
    public class AneksujCzynszCommand : IRequest<AneksujCzynszResult>
    {
        public Guid IdUmowyNajmu { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string KodJednostki { get; set; } = string.Empty;
        public decimal Stawka { get; set; }
        public decimal? IloscBazowa { get; set; }
        public string? KodIndeksacji { get; set; }
        public DateOnly OdDnia { get; set; }
    }
}