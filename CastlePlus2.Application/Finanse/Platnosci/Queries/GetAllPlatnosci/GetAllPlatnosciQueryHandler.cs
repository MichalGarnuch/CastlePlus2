using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Platnosci.Queries.GetAllPlatnosci
{
    public class GetAllPlatnosciQueryHandler : IRequestHandler<GetAllPlatnosciQuery, List<PlatnoscDto>>
    {
        private readonly IPlatnoscRepository _repo;
        private readonly IMapper _mapper;

        public GetAllPlatnosciQueryHandler(IPlatnoscRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PlatnoscDto>> Handle(GetAllPlatnosciQuery request, CancellationToken ct)
        {
            var entities = await _repo.GetAllAsync(ct);
            return entities.Select(e => _mapper.Map<PlatnoscDto>(e)).ToList();
        }
    }
}
