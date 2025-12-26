using AutoMapper;
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Queries.GetAllDokumenty
{
    public class GetAllDokumentyQueryHandler : IRequestHandler<GetAllDokumentyQuery, List<DokumentDto>>
    {
        private readonly IDokumentRepository _repo;
        private readonly IMapper _mapper;

        public GetAllDokumentyQueryHandler(IDokumentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<DokumentDto>> Handle(GetAllDokumentyQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<DokumentDto>(x)).ToList();
        }
    }
}
