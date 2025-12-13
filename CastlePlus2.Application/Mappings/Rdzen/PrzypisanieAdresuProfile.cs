using AutoMapper;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Application.Mappings.Rdzen
{
    public class PrzypisanieAdresuProfile : Profile
    {
        public PrzypisanieAdresuProfile()
        {
            CreateMap<PrzypisanieAdresu, PrzypisanieAdresuDto>()
                .ForMember(d => d.IdPrzypisaniaAdresu, o => o.MapFrom(s => s.IdPrzypisaniaAdresu));

        }
    }
}
