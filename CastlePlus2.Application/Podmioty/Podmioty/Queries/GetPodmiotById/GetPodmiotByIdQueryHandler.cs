using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Podmioty.Queries.GetPodmiotById
{
    public class GetPodmiotByIdQueryHandler : IRequestHandler<GetPodmiotByIdQuery, PodmiotDto?>
    {
        private readonly IPodmiotRepository _repo;
        private readonly IMapper _mapper;

        public GetPodmiotByIdQueryHandler(IPodmiotRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PodmiotDto?> Handle(GetPodmiotByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.IdPodmiotu, ct);
            return entity == null ? null : _mapper.Map<PodmiotDto>(entity);
        }
    }
}
