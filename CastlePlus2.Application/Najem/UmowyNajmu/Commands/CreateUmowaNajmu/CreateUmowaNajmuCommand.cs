using System;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Commands.CreateUmowaNajmu
{
    /// <summary>
    /// Płaskie DTO-command – zgodne 1:1 z kolumnami SQL.
    /// </summary>
    public class CreateUmowaNajmuCommand : IRequest<UmowaNajmuDto>
    {
        public long IdNajemcy { get; set; }
        public long IdWynajmujacego { get; set; }

        public DateTime DataZawarcia { get; set; }
        public DateTime DataPoczatku { get; set; }
        public DateTime? DataZakonczenia { get; set; }

        public string KodWaluty { get; set; } = default!;
        public string? KodIndeksacji { get; set; }
    }
}
