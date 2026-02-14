using AutoMapper;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Domain.Entities.Konfiguracja;

namespace CastlePlus2.Application.Mappings.Konfiguracja
{
    public class ZasobUIProfile : Profile
    {
        public ZasobUIProfile()
        {
            CreateMap<ZasobUI, ZasobUIDto>()
                .ForMember(d => d.IdEncji, o => o.MapFrom(s => s.IdEncji))
                .ForMember(d => d.KodZasobu, o => o.MapFrom(s => s.KodZasobu))
                .ForMember(d => d.Typ, o => o.MapFrom(s => s.Typ))
                .ForMember(d => d.Kategoria, o => o.MapFrom(s => s.Kategoria))
                .ForMember(d => d.CzyAktywny, o => o.MapFrom(s => s.CzyAktywny))
                .ForMember(d => d.Sort, o => o.MapFrom(s => s.Sort))
                .ForMember(d => d.WazneOdUtc, o => o.MapFrom(s => s.WazneOdUtc))
                .ForMember(d => d.WazneDoUtc, o => o.MapFrom(s => s.WazneDoUtc));

            CreateMap<ZasobUITekst, ZasobUITekstDto>()
                .ForMember(d => d.IdZasobuTekstu, o => o.MapFrom(s => s.IdZasobuTekstu))
                .ForMember(d => d.IdEncji, o => o.MapFrom(s => s.IdEncji))
                .ForMember(d => d.Jezyk, o => o.MapFrom(s => s.Jezyk))
                .ForMember(d => d.Pole, o => o.MapFrom(s => s.Pole))
                .ForMember(d => d.Wartosc, o => o.MapFrom(s => s.Wartosc))
                .ForMember(d => d.Format, o => o.MapFrom(s => s.Format))
                .ForMember(d => d.Sort, o => o.MapFrom(s => s.Sort));
        }
    }
}
