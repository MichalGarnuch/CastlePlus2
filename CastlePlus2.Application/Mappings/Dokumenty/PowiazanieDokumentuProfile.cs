using AutoMapper;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Domain.Entities.Dokumenty;

namespace CastlePlus2.Application.Mappings.Dokumenty
{
    public class PowiazanieDokumentuProfile : Profile
    {
        public PowiazanieDokumentuProfile()
        {
            CreateMap<PowiazanieDokumentu, PowiazanieDokumentuDto>();
        }
    }
}
