using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Queries.GetAllLiczniki
{
    public class GetAllLicznikiQueryHandler : IRequestHandler<GetAllLicznikiQuery, List<LicznikDto>>
    {
        private readonly ILicznikRepository _repo;
        private readonly IMapper _mapper;

        public GetAllLicznikiQueryHandler(ILicznikRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<LicznikDto>> Handle(GetAllLicznikiQuery request, CancellationToken ct)
        {
            var entities = await _repo.GetAllAsync(ct);
            return entities
                .Select(e => _mapper.Map<LicznikDto>(e))
                .ToList();
        }
    }
}
