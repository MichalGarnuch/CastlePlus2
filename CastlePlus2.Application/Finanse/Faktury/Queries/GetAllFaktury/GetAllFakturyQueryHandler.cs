using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Queries.GetAllFaktury
{
    public class GetAllFakturyQueryHandler : IRequestHandler<GetAllFakturyQuery, List<FakturaDto>>
    {
        private readonly IFakturaRepository _repo;
        private readonly IMapper _mapper;

        public GetAllFakturyQueryHandler(IFakturaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<FakturaDto>> Handle(GetAllFakturyQuery request, CancellationToken ct)
        {
            var entities = await _repo.GetAllAsync(ct);
            return entities.Select(e => _mapper.Map<FakturaDto>(e)).ToList();
        }
    }
}
