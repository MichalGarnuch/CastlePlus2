using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Podmioty.Queries.GetAllPodmioty
{
    public class GetAllPodmiotyQueryHandler : IRequestHandler<GetAllPodmiotyQuery, List<PodmiotDto>>
    {
        private readonly IPodmiotRepository _repo;
        private readonly IMapper _mapper;

        public GetAllPodmiotyQueryHandler(IPodmiotRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PodmiotDto>> Handle(GetAllPodmiotyQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<PodmiotDto>(x)).ToList();
        }
    }
}
