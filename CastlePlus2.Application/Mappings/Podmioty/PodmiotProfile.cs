using AutoMapper;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Domain.Entities.Podmioty;

namespace CastlePlus2.Application.Mappings.Podmioty
{
    public class PodmiotProfile : Profile
    {
        public PodmiotProfile()
        {
            CreateMap<Podmiot, PodmiotDto>();
        }
    }
}
