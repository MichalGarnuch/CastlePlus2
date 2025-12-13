using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.PozycjeKosztow.Queries.GetPozycjaKosztuById
{
    public class GetPozycjaKosztuByIdQueryHandler
        : IRequestHandler<GetPozycjaKosztuByIdQuery, PozycjaKosztuDto?>
    {
        private readonly IPozycjaKosztuRepository _repo;
        private readonly IMapper _mapper;

        public GetPozycjaKosztuByIdQueryHandler(IPozycjaKosztuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PozycjaKosztuDto?> Handle(GetPozycjaKosztuByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.Id, ct);
            return entity is null ? null : _mapper.Map<PozycjaKosztuDto>(entity);
        }
    }
}
