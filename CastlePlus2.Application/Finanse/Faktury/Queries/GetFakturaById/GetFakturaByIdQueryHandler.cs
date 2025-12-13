using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Queries.GetFakturaById
{
    public class GetFakturaByIdQueryHandler : IRequestHandler<GetFakturaByIdQuery, FakturaDto?>
    {
        private readonly IFakturaRepository _repo;
        private readonly IMapper _mapper;

        public GetFakturaByIdQueryHandler(IFakturaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<FakturaDto?> Handle(GetFakturaByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.IdFaktury, ct);
            return entity is null ? null : _mapper.Map<FakturaDto>(entity);
        }
    }
}
