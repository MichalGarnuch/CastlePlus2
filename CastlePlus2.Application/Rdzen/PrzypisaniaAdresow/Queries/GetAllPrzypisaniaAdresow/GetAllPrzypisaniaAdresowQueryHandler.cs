using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Queries.GetAllPrzypisaniaAdresow
{
    public sealed class GetAllPrzypisaniaAdresowQueryHandler
        : IRequestHandler<GetAllPrzypisaniaAdresowQuery, List<PrzypisanieAdresuDto>>
    {
        private readonly IPrzypisanieAdresuRepository _repo;
        private readonly IMapper _mapper;

        public GetAllPrzypisaniaAdresowQueryHandler(IPrzypisanieAdresuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PrzypisanieAdresuDto>> Handle(GetAllPrzypisaniaAdresowQuery request, CancellationToken ct)
        {
            var entities = await _repo.GetAllAsync(ct);
            return entities.Select(_mapper.Map<PrzypisanieAdresuDto>).ToList();
        }
    }
}
