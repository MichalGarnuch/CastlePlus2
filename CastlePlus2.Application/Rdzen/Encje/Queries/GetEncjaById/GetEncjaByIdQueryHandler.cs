using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Encje.Queries.GetEncjaById
{
    public class GetEncjaByIdQueryHandler : IRequestHandler<GetEncjaByIdQuery, EncjaDto?>
    {
        private readonly IEncjaRepository _repo;
        private readonly IMapper _mapper;

        public GetEncjaByIdQueryHandler(IEncjaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<EncjaDto?> Handle(GetEncjaByIdQuery request, CancellationToken ct)
        {
            if (request.Id == Guid.Empty)
            {
                return null;
            }

            var entity = await _repo.GetByIdAsync(request.Id, ct);
            return entity is null ? null : _mapper.Map<EncjaDto>(entity);
        }
    }
}