using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Queries.GetWlasnosciByEncjaId
{
    public class GetWlasnosciByEncjaIdQueryHandler : IRequestHandler<GetWlasnosciByEncjaIdQuery, IReadOnlyList<WlasnoscDto>>
    {
        private readonly IWlasnoscRepository _repo;
        private readonly IMapper _mapper;

        public GetWlasnosciByEncjaIdQueryHandler(IWlasnoscRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<WlasnoscDto>> Handle(GetWlasnosciByEncjaIdQuery request, CancellationToken ct)
        {
            if (request.IdEncji == System.Guid.Empty)
                return new List<WlasnoscDto>();

            var list = await _repo.GetByEncjaIdAsync(request.IdEncji, ct);
            return list.Select(x => _mapper.Map<WlasnoscDto>(x)).ToList();
        }
    }
}
