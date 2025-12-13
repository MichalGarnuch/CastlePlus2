using AutoMapper;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Domain.Entities.Finanse;

namespace CastlePlus2.Application.Mappings.Finanse
{
    public class PlatnoscProfile : Profile
    {
        public PlatnoscProfile()
        {
            CreateMap<Platnosc, PlatnoscDto>();
        }
    }
}
