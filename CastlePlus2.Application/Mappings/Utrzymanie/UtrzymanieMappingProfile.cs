using AutoMapper;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Domain.Entities.Utrzymanie;

namespace CastlePlus2.Application.Mappings.Utrzymanie
{
    public class UtrzymanieMappingProfile : Profile
    {
        public UtrzymanieMappingProfile()
        {
            CreateMap<ZleceniePracy, ZleceniePracyDto>();
        }
    }
}
