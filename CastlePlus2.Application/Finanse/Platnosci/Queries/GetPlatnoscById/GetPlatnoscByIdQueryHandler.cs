using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Platnosci.Queries.GetPlatnoscById
{
    public class GetPlatnoscByIdQueryHandler : IRequestHandler<GetPlatnoscByIdQuery, PlatnoscDto?>
    {
        private readonly IPlatnoscRepository _repo;
        private readonly IMapper _mapper;

        public GetPlatnoscByIdQueryHandler(IPlatnoscRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PlatnoscDto?> Handle(GetPlatnoscByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.Id, ct);
            return entity is null ? null : _mapper.Map<PlatnoscDto>(entity);
        }
    }
}
