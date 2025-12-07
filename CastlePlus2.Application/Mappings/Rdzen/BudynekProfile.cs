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
    /// Profil mapowania dla encji Budynek.
    /// </summary>
    public class BudynekProfile : Profile
    {
        public BudynekProfile()
        {
            // Mapowanie encja -> DTO
            CreateMap<Budynek, BudynekDto>();
        }
    }
}
