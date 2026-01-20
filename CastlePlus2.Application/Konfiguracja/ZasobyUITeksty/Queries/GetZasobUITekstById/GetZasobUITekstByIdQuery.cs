using CastlePlus2.Contracts.DTOs.Konfiguracja;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Queries.GetZasobUITekstById
{
    public sealed record GetZasobUITekstByIdQuery(long IdZasobuTekstu) : IRequest<ZasobUITekstDto?>;
}
