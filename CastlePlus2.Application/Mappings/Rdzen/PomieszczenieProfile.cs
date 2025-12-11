using AutoMapper;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Application.Mappings.Rdzen
{
    /// <summary>
    /// Mapowania dla encji Pomieszczenie.
    /// </summary>
    public class PomieszczenieProfile : Profile
    {
        public PomieszczenieProfile()
        {
            CreateMap<Pomieszczenie, PomieszczenieDto>()
                // PK DTO = Id (Guid) z Encja
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
        }
    }
}
