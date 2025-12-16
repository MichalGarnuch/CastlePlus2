using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.RodzajeMediow.Queries.GetRodzajeMediow
{
    public sealed class GetRodzajeMediowQueryHandler : IRequestHandler<GetRodzajeMediowQuery, List<RodzajMediumDto>>
    {
        private readonly IRodzajMediumRepository _repo;
        private readonly IMapper _mapper;

        public GetRodzajeMediowQueryHandler(IRodzajMediumRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<RodzajMediumDto>> Handle(GetRodzajeMediowQuery request, CancellationToken ct)
        {
            var entities = await _repo.GetAllAsync(ct);
            return _mapper.Map<List<RodzajMediumDto>>(entities);
        }
    }
}
