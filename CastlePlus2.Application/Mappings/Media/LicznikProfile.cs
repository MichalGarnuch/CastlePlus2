using AutoMapper;
using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Domain.Entities.Media;

namespace CastlePlus2.Application.Mappings.Media
{
    public class LicznikProfile : Profile
    {
        public LicznikProfile()
        {
            CreateMap<Licznik, LicznikDto>();
        }
    }
}
