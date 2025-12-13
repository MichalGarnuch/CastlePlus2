using AutoMapper;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Domain.Entities.Utrzymanie;

namespace CastlePlus2.Application.Mappings.Utrzymanie
{
    public class ZleceniePracyProfile : Profile
    {
        public ZleceniePracyProfile()
        {
            CreateMap<ZleceniePracy, ZleceniePracyDto>();
        }
    }
}
