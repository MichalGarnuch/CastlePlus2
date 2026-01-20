using AutoMapper;
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Queries.GetDokumentyByEncjaId
{
    public class GetDokumentyByEncjaIdQueryHandler : IRequestHandler<GetDokumentyByEncjaIdQuery, List<DokumentDto>>
    {
        private readonly IPowiazanieDokumentuRepository _repo;
        private readonly IMapper _mapper;

        public GetDokumentyByEncjaIdQueryHandler(IPowiazanieDokumentuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<DokumentDto>> Handle(GetDokumentyByEncjaIdQuery request, CancellationToken ct)
        {
            var list = await _repo.GetDokumentyByEncjaIdAsync(request.IdEncji, ct);
            return list.Select(x => _mapper.Map<DokumentDto>(x)).ToList();
        }
    }
}