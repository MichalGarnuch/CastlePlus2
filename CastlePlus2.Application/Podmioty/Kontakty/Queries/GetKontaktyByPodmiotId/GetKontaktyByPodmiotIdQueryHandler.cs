using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Kontakty.Queries.GetKontaktyByPodmiotId
{
    public class GetKontaktyByPodmiotIdQueryHandler : IRequestHandler<GetKontaktyByPodmiotIdQuery, List<KontaktDto>>
    {
        private readonly IKontaktRepository _repo;
        private readonly IMapper _mapper;

        public GetKontaktyByPodmiotIdQueryHandler(IKontaktRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<KontaktDto>> Handle(GetKontaktyByPodmiotIdQuery request, CancellationToken ct)
        {
            if (request.IdPodmiotu <= 0)
                return new List<KontaktDto>();

            var list = await _repo.GetByPodmiotIdAsync(request.IdPodmiotu, ct);
            return _mapper.Map<List<KontaktDto>>(list);
        }
    }
}
