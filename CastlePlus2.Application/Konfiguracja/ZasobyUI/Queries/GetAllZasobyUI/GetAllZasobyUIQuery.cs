using CastlePlus2.Contracts.DTOs.Konfiguracja;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUI.Queries.GetAllZasobyUI
{
    public class GetAllZasobyUIQuery : IRequest<List<ZasobUIDto>>
    {
        public string? Typ { get; set; }
        public string? Kategoria { get; set; }
        public bool? CzyAktywny { get; set; }
    }
}