using AutoMapper;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Domain.Entities.Konfiguracja;

namespace CastlePlus2.Application.Mappings.Konfiguracja
{
    public class ZasobUIProfile : Profile
    {
        public ZasobUIProfile()
        {
            CreateMap<ZasobUI, ZasobUIDto>();
            CreateMap<ZasobUITekst, ZasobUITekstDto>();
        }
    }
}
