using AutoMapper;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Application.Mappings.Rdzen
{
    /// <summary>
    /// Profil mapowania dla encji Lokal.
    /// </summary>
    public class LokalProfile : Profile
    {
        public LokalProfile()
        {
            // Encja -> DTO
            CreateMap<Lokal, LokalDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
        }
    }
}
