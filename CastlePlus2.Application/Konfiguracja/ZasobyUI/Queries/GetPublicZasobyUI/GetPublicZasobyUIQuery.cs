using CastlePlus2.Contracts.DTOs.Konfiguracja;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUI.Queries.GetPublicZasobyUI
{
    public class GetPublicZasobyUIQuery : IRequest<List<ZasobUIPublicDto>>
    {
        public string Typ { get; set; } = string.Empty;
        public string? Kategoria { get; set; }
        public string Jezyk { get; set; } = "pl-PL";
        public bool IncludeInactive { get; set; }
    }
}