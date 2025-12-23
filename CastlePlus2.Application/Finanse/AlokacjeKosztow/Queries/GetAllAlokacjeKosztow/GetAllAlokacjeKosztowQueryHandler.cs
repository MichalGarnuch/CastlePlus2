using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.AlokacjeKosztow.Queries.GetAllAlokacjeKosztow
{
    public class GetAllAlokacjeKosztowQueryHandler : IRequestHandler<GetAllAlokacjeKosztowQuery, List<AlokacjaKosztuDto>>
    {
        private readonly IAlokacjaKosztuRepository _repo;
        private readonly IMapper _mapper;

        public GetAllAlokacjeKosztowQueryHandler(IAlokacjaKosztuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<AlokacjaKosztuDto>> Handle(GetAllAlokacjeKosztowQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<AlokacjaKosztuDto>(x)).ToList();
        }
    }
}
