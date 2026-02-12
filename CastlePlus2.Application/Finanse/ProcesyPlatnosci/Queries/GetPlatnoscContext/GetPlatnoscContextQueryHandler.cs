using AutoMapper;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;
using System.Linq;

namespace CastlePlus2.Application.Finanse.ProcesyPlatnosci.Queries.GetPlatnoscContext
{
    public class GetPlatnoscContextQueryHandler : IRequestHandler<GetPlatnoscContextQuery, PlatnoscContextDto>
    {
        private readonly IWalutaRepository _walutaRepository;
        private readonly IMapper _mapper;

        public GetPlatnoscContextQueryHandler(
            IWalutaRepository walutaRepository,
            IMapper mapper)
        {
            _walutaRepository = walutaRepository;
            _mapper = mapper;
        }

        public async Task<PlatnoscContextDto> Handle(GetPlatnoscContextQuery request, CancellationToken ct)
        {
            var waluty = await _walutaRepository.GetAllAsync(ct);

            return new PlatnoscContextDto
            {
                Podmioty = new List<PodmiotDto>(),
                Waluty = waluty.Select(x => _mapper.Map<WalutaDto>(x)).OrderBy(x => x.KodWaluty).ToList(),
                Faktury = new List<FakturaDoRozliczeniaDto>()
            };
        }
    }
}