using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Queries.GetAllBudynki
{
    public sealed class GetAllBudynkiQueryHandler : IRequestHandler<GetAllBudynkiQuery, List<BudynekDto>>
    {
        private readonly IBudynekRepository _repo;
        private readonly IMapper _mapper;

        public GetAllBudynkiQueryHandler(IBudynekRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<BudynekDto>> Handle(GetAllBudynkiQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(_mapper.Map<BudynekDto>).ToList();
        }
    }
}
