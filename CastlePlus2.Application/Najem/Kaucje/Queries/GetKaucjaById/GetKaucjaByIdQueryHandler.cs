using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Kaucje.Queries.GetKaucjaById
{
    public class GetKaucjaByIdQueryHandler : IRequestHandler<GetKaucjaByIdQuery, KaucjaDto?>
    {
        private readonly IKaucjaRepository _repo;
        private readonly IMapper _mapper;

        public GetKaucjaByIdQueryHandler(IKaucjaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<KaucjaDto?> Handle(GetKaucjaByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.Id, ct);
            return entity == null ? null : _mapper.Map<KaucjaDto>(entity);
        }
    }
}
