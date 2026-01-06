using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Queries.GetWlasnoscContext
{
    public class GetWlasnoscContextQueryHandler : IRequestHandler<GetWlasnoscContextQuery, WlasnoscContextDto>
    {
        private readonly IEncjaRepository _encjaRepository;
        private readonly IPodmiotRepository _podmiotRepository;
        private readonly IMapper _mapper;

        public GetWlasnoscContextQueryHandler(
            IEncjaRepository encjaRepository,
            IPodmiotRepository podmiotRepository,
            IMapper mapper)
        {
            _encjaRepository = encjaRepository;
            _podmiotRepository = podmiotRepository;
            _mapper = mapper;
        }

        public async Task<WlasnoscContextDto> Handle(GetWlasnoscContextQuery request, CancellationToken ct)
        {
            var encje = await _encjaRepository.GetAllAsync(ct);
            var podmioty = await _podmiotRepository.GetAllAsync(ct);

            return new WlasnoscContextDto
            {
                Encje = encje.Select(x => _mapper.Map<EncjaDto>(x))
                    .OrderBy(x => x.TypEncji)
                    .ThenBy(x => x.KodEncji)
                    .ToList(),
                Podmioty = podmioty.Select(x => _mapper.Map<PodmiotDto>(x))
                    .OrderBy(x => x.Nazwa)
                    .ToList()
            };
        }
    }
}
