using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Queries.GetRozliczeniePlatnosciById
{
    public class GetRozliczeniePlatnosciByIdQueryHandler
        : IRequestHandler<GetRozliczeniePlatnosciByIdQuery, RozliczeniePlatnosciDto?>
    {
        private readonly IRozliczeniePlatnosciRepository _repo;
        private readonly IMapper _mapper;

        public GetRozliczeniePlatnosciByIdQueryHandler(IRozliczeniePlatnosciRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RozliczeniePlatnosciDto?> Handle(GetRozliczeniePlatnosciByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.Id, ct);
            return entity is null ? null : _mapper.Map<RozliczeniePlatnosciDto>(entity);
        }
    }
}
