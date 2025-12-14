using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.SkladnikiCzynszu.Queries.GetAllSkladnikiCzynszu
{
    public class GetAllSkladnikiCzynszuQueryHandler : IRequestHandler<GetAllSkladnikiCzynszuQuery, List<SkladnikCzynszuDto>>
    {
        private readonly ISkladnikCzynszuRepository _repo;
        private readonly IMapper _mapper;

        public GetAllSkladnikiCzynszuQueryHandler(ISkladnikCzynszuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<SkladnikCzynszuDto>> Handle(GetAllSkladnikiCzynszuQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<SkladnikCzynszuDto>(x)).ToList();
        }
    }
}
