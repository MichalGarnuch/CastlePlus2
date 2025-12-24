using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.PozycjeKosztow.Queries.GetAllPozycjeKosztow
{
    public class GetAllPozycjeKosztowQueryHandler : IRequestHandler<GetAllPozycjeKosztowQuery, List<PozycjaKosztuDto>>
    {
        private readonly IPozycjaKosztuRepository _repo;
        private readonly IMapper _mapper;

        public GetAllPozycjeKosztowQueryHandler(IPozycjaKosztuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PozycjaKosztuDto>> Handle(GetAllPozycjeKosztowQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<PozycjaKosztuDto>(x)).ToList();
        }
    }
}
