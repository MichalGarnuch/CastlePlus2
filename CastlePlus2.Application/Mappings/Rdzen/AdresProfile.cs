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
    /// Profil mapowania dla encji Adres.
    /// </summary>
    public class AdresProfile : Profile
    {
        public AdresProfile()
        {
            // Encja -> DTO
            CreateMap<Adres, AdresDto>();
        }
    }
}

