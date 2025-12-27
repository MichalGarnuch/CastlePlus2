using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Queries.GetBudynkiByNieruchomoscId
{
    public sealed class GetBudynkiByNieruchomoscIdQueryHandler
        : IRequestHandler<GetBudynkiByNieruchomoscIdQuery, List<BudynekDto>>
    {
        private readonly IBudynekRepository _repo;
        private readonly IMapper _mapper;

        public GetBudynkiByNieruchomoscIdQueryHandler(IBudynekRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<BudynekDto>> Handle(GetBudynkiByNieruchomoscIdQuery request, CancellationToken ct)
        {
            var all = await _repo.GetAllAsync(ct);

            return all
                .Where(x => x.IdNieruchomosci == request.IdNieruchomosci)
                .Select(_mapper.Map<BudynekDto>)
                .ToList();
        }
    }
}
