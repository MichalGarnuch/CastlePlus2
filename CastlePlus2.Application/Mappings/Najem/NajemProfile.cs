using AutoMapper;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Domain.Entities.Najem;

namespace CastlePlus2.Application.Mappings.Najem
{
    public class NajemProfile : Profile
    {
        public NajemProfile()
        {
            CreateMap<UmowaNajmu, UmowaNajmuDto>();
            CreateMap<PrzedmiotNajmu, PrzedmiotNajmuDto>();
            CreateMap<SkladnikCzynszu, SkladnikCzynszuDto>();
            CreateMap<Kaucja, KaucjaDto>();
        }
    }
}
