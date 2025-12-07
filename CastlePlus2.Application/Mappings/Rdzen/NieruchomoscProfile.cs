using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Application.Mappings.Rdzen
{
    /// <summary>
    /// Profil AutoMappera definiujący mapowania między:
    /// - encją domenową Nieruchomosc
    /// - DTO NieruchomoscDto
    /// oraz encją Adres -> AdresDto.
    /// </summary>
    public class NieruchomoscProfile : Profile
    {
        public NieruchomoscProfile()
        {
            // Mapowanie encji Adres -> AdresDto
            CreateMap<Adres, AdresDto>();

            // Mapowanie jednostronne: encja -> DTO (Nieruchomosc -> NieruchomoscDto)
            CreateMap<Nieruchomosc, NieruchomoscDto>()
                // Jawnie wskazujemy, że AdresGlowny w DTO ma pochodzić z nawigacji
                .ForMember(dest => dest.AdresGlowny,
                           opt => opt.MapFrom(src => src.AdresGlowny));
        }
    }
}
