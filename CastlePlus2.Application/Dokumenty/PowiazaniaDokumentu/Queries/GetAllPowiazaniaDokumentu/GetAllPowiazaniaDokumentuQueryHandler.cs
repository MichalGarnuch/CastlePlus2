using AutoMapper;
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Queries.GetAllPowiazaniaDokumentu
{
    public class GetAllPowiazaniaDokumentuQueryHandler
        : IRequestHandler<GetAllPowiazaniaDokumentuQuery, List<PowiazanieDokumentuDto>>
    {
        private readonly IPowiazanieDokumentuRepository _repo;
        private readonly IMapper _mapper;

        public GetAllPowiazaniaDokumentuQueryHandler(IPowiazanieDokumentuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PowiazanieDokumentuDto>> Handle(GetAllPowiazaniaDokumentuQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<PowiazanieDokumentuDto>(x)).ToList();
        }
    }
}
