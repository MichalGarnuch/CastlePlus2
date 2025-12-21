using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Lokale.Queries.GetAllLokale
{
    public sealed class GetAllLokaleQueryHandler : IRequestHandler<GetAllLokaleQuery, List<LokalDto>>
    {
        private readonly ILokalRepository _repo;
        private readonly IMapper _mapper;

        public GetAllLokaleQueryHandler(ILokalRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<LokalDto>> Handle(GetAllLokaleQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(_mapper.Map<LokalDto>).ToList();
        }
    }
}
