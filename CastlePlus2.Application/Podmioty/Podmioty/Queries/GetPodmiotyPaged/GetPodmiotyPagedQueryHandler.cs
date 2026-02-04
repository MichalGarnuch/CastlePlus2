using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Podmioty.Queries.GetPodmiotyPaged
{
    public class GetPodmiotyPagedQueryHandler : IRequestHandler<GetPodmiotyPagedQuery, PodmiotPagedResultDto>
    {
        private readonly IPodmiotRepository _repo;
        private readonly IMapper _mapper;

        public GetPodmiotyPagedQueryHandler(IPodmiotRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PodmiotPagedResultDto> Handle(GetPodmiotyPagedQuery request, CancellationToken ct)
        {
            var page = request.Page < 1 ? 1 : request.Page;
            var pageSize = request.PageSize <= 0 ? 20 : request.PageSize;

            var (items, total) = await _repo.GetPagedAsync(
                page,
                pageSize,
                request.SearchTerm,
                request.SortBy,
                request.SortDesc,
                ct);

            return new PodmiotPagedResultDto
            {
                Items = items.Select(x => _mapper.Map<PodmiotDto>(x)).ToList(),
                TotalCount = total
            };
        }
    }
}