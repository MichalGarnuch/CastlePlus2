using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Encje.Queries.GetAllEncje
{
    public class GetAllEncjeQueryHandler : IRequestHandler<GetAllEncjeQuery, List<EncjaDto>>
    {
        private readonly IEncjaRepository _repo;
        private readonly IMapper _mapper;

        public GetAllEncjeQueryHandler(IEncjaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<EncjaDto>> Handle(GetAllEncjeQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(e => _mapper.Map<EncjaDto>(e)).ToList();
        }
    }
}