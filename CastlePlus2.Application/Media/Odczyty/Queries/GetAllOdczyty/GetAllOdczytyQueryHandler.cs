using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Queries.GetAllOdczyty
{
    public class GetAllOdczytyQueryHandler : IRequestHandler<GetAllOdczytyQuery, List<OdczytDto>>
    {
        private readonly IOdczytRepository _repo;
        private readonly IMapper _mapper;

        public GetAllOdczytyQueryHandler(IOdczytRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<OdczytDto>> Handle(GetAllOdczytyQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<OdczytDto>(x)).ToList();
        }
    }
}
