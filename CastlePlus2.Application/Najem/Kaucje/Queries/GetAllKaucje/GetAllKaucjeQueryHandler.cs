using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Kaucje.Queries.GetAllKaucje
{
    public class GetAllKaucjeQueryHandler : IRequestHandler<GetAllKaucjeQuery, List<KaucjaDto>>
    {
        private readonly IKaucjaRepository _repo;
        private readonly IMapper _mapper;

        public GetAllKaucjeQueryHandler(IKaucjaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<KaucjaDto>> Handle(GetAllKaucjeQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<KaucjaDto>(x)).ToList();
        }
    }
}
