using System;
using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Commands.UpdateOdczyt
{
    // Standard: Command płaski, Controller mapuje Request -> Command
    public sealed class UpdateOdczytCommand : IRequest<bool>
    {
        public long IdOdczytu { get; set; }
        public long IdLicznika { get; set; }
        public DateTime DataOdczytu { get; set; }
        public decimal Wskazanie { get; set; }
        public string? Zrodlo { get; set; }
    }
}