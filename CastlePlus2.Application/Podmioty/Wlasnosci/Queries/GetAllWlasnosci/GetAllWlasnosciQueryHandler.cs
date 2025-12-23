using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Queries.GetAllWlasnosci
{
    public class GetAllWlasnosciQueryHandler : IRequestHandler<GetAllWlasnosciQuery, List<WlasnoscDto>>
    {
        private readonly IWlasnoscRepository _repo;
        private readonly IMapper _mapper;

        public GetAllWlasnosciQueryHandler(IWlasnoscRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<WlasnoscDto>> Handle(GetAllWlasnosciQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(_mapper.Map<WlasnoscDto>).ToList();
        }
    }
}