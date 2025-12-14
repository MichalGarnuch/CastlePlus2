using AutoMapper;
using CastlePlus2.Contracts.DTOs.Slowniki;
using CastlePlus2.Domain.Entities.Slowniki;

namespace CastlePlus2.Application.Mappings.Slowniki
{
    public class IndeksacjaProfile : Profile
    {
        public IndeksacjaProfile()
        {
            CreateMap<Indeksacja, IndeksacjaDto>();
        }
    }
}
