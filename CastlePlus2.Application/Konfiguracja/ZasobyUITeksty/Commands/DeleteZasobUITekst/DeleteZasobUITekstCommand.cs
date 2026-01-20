using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Commands.DeleteZasobUITekst
{
    public sealed record DeleteZasobUITekstCommand(long IdZasobuTekstu) : IRequest<bool>;
}
