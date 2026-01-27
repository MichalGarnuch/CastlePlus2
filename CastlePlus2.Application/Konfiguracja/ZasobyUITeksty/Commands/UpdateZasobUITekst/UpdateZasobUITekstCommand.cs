using MediatR;
using System;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Commands.UpdateZasobUITekst
{
    public class UpdateZasobUITekstCommand : IRequest<bool>
    {
        public long IdZasobuTekstu { get; set; }
        public Guid IdEncji { get; set; }

        public string Jezyk { get; set; } = string.Empty;
        public string Pole { get; set; } = string.Empty;
        public string Wartosc { get; set; } = string.Empty;
        public string? Format { get; set; }
        public int Sort { get; set; }
    }
}