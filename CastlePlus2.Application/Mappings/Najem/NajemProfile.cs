using AutoMapper;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Domain.Entities.Najem;

namespace CastlePlus2.Application.Mappings.Najem
{
    public class NajemProfile : Profile
    {
        public NajemProfile()
        {
            CreateMap<UmowaNajmu, UmowaNajmuDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));

            CreateMap<PrzedmiotNajmu, PrzedmiotNajmuDto>();
            CreateMap<SkladnikCzynszu, SkladnikCzynszuDto>();
            CreateMap<Kaucja, KaucjaDto>();
        }
    }
}
