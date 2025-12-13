using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.AlokacjeKosztow.Queries.GetAlokacjaKosztuById
{
    public class GetAlokacjaKosztuByIdQueryHandler
        : IRequestHandler<GetAlokacjaKosztuByIdQuery, AlokacjaKosztuDto?>
    {
        private readonly IAlokacjaKosztuRepository _repo;
        private readonly IMapper _mapper;

        public GetAlokacjaKosztuByIdQueryHandler(IAlokacjaKosztuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AlokacjaKosztuDto?> Handle(GetAlokacjaKosztuByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.IdAlokacji, ct);
            return entity is null ? null : _mapper.Map<AlokacjaKosztuDto>(entity);
        }
    }
}
