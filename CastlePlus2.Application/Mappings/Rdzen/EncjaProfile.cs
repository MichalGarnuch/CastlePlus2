using AutoMapper;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Application.Mappings.Rdzen
{
    /// <summary>
    /// Profil mapowania dla encji Encja.
    /// </summary>
    public class EncjaProfile : Profile
    {
        public EncjaProfile()
        {
            CreateMap<Encja, EncjaDto>();
        }
    }
}