using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Application.Interfaces.Rdzen;
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
        private readonly IPodmiotRepository _podmiotRepository;
        private readonly IWalutaRepository _walutaRepository;
        private readonly IKategoriaKosztuRepository _kategoriaRepository;
        private readonly IEncjaRepository _encjaRepository;
        private readonly IMapper _mapper;

        public GetWystawFaktureContextQueryHandler(
            IPodmiotRepository podmiotRepository,
            IWalutaRepository walutaRepository,
            IKategoriaKosztuRepository kategoriaRepository,
            IEncjaRepository encjaRepository,
            IMapper mapper)
        {
            _podmiotRepository = podmiotRepository;
            _walutaRepository = walutaRepository;
            _kategoriaRepository = kategoriaRepository;
            _encjaRepository = encjaRepository;
            _mapper = mapper;
        }

        public async Task<WystawFaktureContextDto> Handle(GetWystawFaktureContextQuery request, CancellationToken ct)
        {
            var podmioty = await _podmiotRepository.GetAllAsync(ct);
            var waluty = await _walutaRepository.GetAllAsync(ct);
            var kategorie = await _kategoriaRepository.GetAllAsync(ct);
            var encje = await _encjaRepository.GetAllAsync(ct);

            var encjeLookup = encje
                .OrderBy(e => e.TypEncji)
                .ThenBy(e => e.KodEncji)
                .Take(500)
                .Select(e =>
                {
                    var label = string.IsNullOrWhiteSpace(e.KodEncji)
                        ? e.TypEncji
                        : $"{e.TypEncji} / {e.KodEncji}";

                    return new EncjaLookupDto
                    {
                        IdEncji = e.Id,
                        TypEncji = e.TypEncji,
                        KodEncji = e.KodEncji,
                        Label = label
                    };
                })
                .ToList();

            return new WystawFaktureContextDto
            {
                Podmioty = podmioty.Select(x => _mapper.Map<PodmiotDto>(x)).OrderBy(x => x.Nazwa).ToList(),
                Waluty = waluty.Select(x => _mapper.Map<WalutaDto>(x)).OrderBy(x => x.KodWaluty).ToList(),
                KategorieKosztow = kategorie.Select(x => _mapper.Map<KategoriaKosztuDto>(x)).OrderBy(x => x.Kod).ToList(),
                Encje = encjeLookup
            };
        }
    }
}