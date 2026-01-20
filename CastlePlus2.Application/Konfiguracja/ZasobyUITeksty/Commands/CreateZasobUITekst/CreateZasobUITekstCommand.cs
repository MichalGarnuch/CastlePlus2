using CastlePlus2.Contracts.DTOs.Konfiguracja;
using MediatR;
using System;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Commands.CreateZasobUITekst
{
    public class CreateZasobUITekstCommand : IRequest<ZasobUITekstDto>
    {
        public Guid IdEncji { get; set; }
        public string Jezyk { get; set; } = string.Empty;
        public string Pole { get; set; } = string.Empty;
        public string Wartosc { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
    }
}