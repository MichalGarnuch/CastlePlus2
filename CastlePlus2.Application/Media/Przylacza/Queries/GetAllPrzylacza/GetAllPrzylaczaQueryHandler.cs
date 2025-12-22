using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Queries.GetAllPrzylacza
{
    public sealed class GetAllPrzylaczaQueryHandler : IRequestHandler<GetAllPrzylaczaQuery, List<PrzylaczeDto>>
    {
        private readonly IPrzylaczeRepository _repo;
        private readonly IMapper _mapper;

        public GetAllPrzylaczaQueryHandler(IPrzylaczeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PrzylaczeDto>> Handle(GetAllPrzylaczaQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return _mapper.Map<List<PrzylaczeDto>>(list);
        }
    }
}
