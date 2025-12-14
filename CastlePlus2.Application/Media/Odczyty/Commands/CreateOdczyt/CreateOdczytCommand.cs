using System;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Commands.CreateOdczyt
{
    public class CreateOdczytCommand : IRequest<OdczytDto>
    {
        public long IdLicznika { get; set; }
        public DateTime DataOdczytu { get; set; } // w Swagger wkleisz "2025-12-14"
        public decimal Wskazanie { get; set; }
        public string? Zrodlo { get; set; }
    }
}
