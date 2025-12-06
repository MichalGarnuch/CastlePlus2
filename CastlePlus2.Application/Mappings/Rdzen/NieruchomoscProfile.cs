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
    /// 
    /// AutoMapper automatycznie dopasowuje właściwości po nazwie i typie.
    /// Tutaj dodajemy reguły mapowania oraz ewentualne konwersje.
    /// </summary>
    public class NieruchomoscProfile : Profile
    {
        public NieruchomoscProfile()
        {
            // Mapowanie jednostronne: encja -> DTO
            // Używane np. w zapytaniach GET.
            CreateMap<Nieruchomosc, NieruchomoscDto>();

            // Opcjonalnie można dodać mapowanie DTO -> encja,
            // jeśli będziemy używać AutoMapper w komendach update.
            //
            // CreateMap<NieruchomoscDto, Nieruchomosc>()
            //     .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id nie zmieniamy przy update
        }
    }
}

