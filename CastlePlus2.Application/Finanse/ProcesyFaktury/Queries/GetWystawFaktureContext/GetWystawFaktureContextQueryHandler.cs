using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;
using System.Linq;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Queries.GetWystawFaktureContext
{
    public class GetWystawFaktureContextQueryHandler
        : IRequestHandler<GetWystawFaktureContextQuery, WystawFaktureContextDto>
    {
        private readonly IWalutaRepository _walutaRepository;
        private readonly IKategoriaKosztuRepository _kategoriaRepository;
        private readonly IMapper _mapper;

        public GetWystawFaktureContextQueryHandler(
            IWalutaRepository walutaRepository,
            IKategoriaKosztuRepository kategoriaRepository,
            IMapper mapper)
        {
            _walutaRepository = walutaRepository;
            _kategoriaRepository = kategoriaRepository;
            _mapper = mapper;
        }

        public async Task<WystawFaktureContextDto> Handle(GetWystawFaktureContextQuery request, CancellationToken ct)
        {
            var waluty = await _walutaRepository.GetAllAsync(ct);
            var kategorie = await _kategoriaRepository.GetAllAsync(ct);

            return new WystawFaktureContextDto
            {
                Podmioty = new List<PodmiotDto>(),
                Waluty = waluty.Select(x => _mapper.Map<WalutaDto>(x)).OrderBy(x => x.KodWaluty).ToList(),
                KategorieKosztow = kategorie.Select(x => _mapper.Map<KategoriaKosztuDto>(x)).OrderBy(x => x.Kod).ToList(),
                Encje = new List<EncjaLookupDto>()
            };
        }
    }
}