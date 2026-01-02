using System;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Commands.ZawrzUmoweNajmu
{
    public class ZawrzUmoweNajmuCommand : IRequest<ZawrzUmoweNajmuResult>
    {
        public Guid IdLokalu { get; set; }
        public long IdWynajmujacego { get; set; }
        public long IdNajemcy { get; set; }
        public DateOnly DataZawarcia { get; set; }
        public DateOnly DataPoczatku { get; set; }
        public DateOnly? DataZakonczenia { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public string? KodIndeksacji { get; set; }
        public string NazwaCzynszu { get; set; } = string.Empty;
        public string KodJednostki { get; set; } = string.Empty;
        public decimal Stawka { get; set; }
        public decimal? IloscBazowa { get; set; }
        public decimal? KwotaKaucji { get; set; }
    }
}