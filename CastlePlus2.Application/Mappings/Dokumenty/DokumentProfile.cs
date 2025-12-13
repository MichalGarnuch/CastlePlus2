using AutoMapper;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Domain.Entities.Dokumenty;

namespace CastlePlus2.Application.Mappings.Dokumenty
{
    public class DokumentProfile : Profile
    {
        public DokumentProfile()
        {
            CreateMap<Dokument, DokumentDto>();
        }
    }
}
