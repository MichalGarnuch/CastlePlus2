using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Application.Interfaces.Podmioty;
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
        private readonly IFakturaRepository _fakturaRepository;
        private readonly IRozliczeniePlatnosciRepository _rozliczenieRepository;
        private readonly IPodmiotRepository _podmiotRepository;
        private readonly IWalutaRepository _walutaRepository;
        private readonly IMapper _mapper;

        public GetPlatnoscContextQueryHandler(
            IFakturaRepository fakturaRepository,
            IRozliczeniePlatnosciRepository rozliczenieRepository,
            IPodmiotRepository podmiotRepository,
            IWalutaRepository walutaRepository,
            IMapper mapper)
        {
            _fakturaRepository = fakturaRepository;
            _rozliczenieRepository = rozliczenieRepository;
            _podmiotRepository = podmiotRepository;
            _walutaRepository = walutaRepository;
            _mapper = mapper;
        }

        public async Task<PlatnoscContextDto> Handle(GetPlatnoscContextQuery request, CancellationToken ct)
        {
            var faktury = await _fakturaRepository.GetAllAsync(ct);
            var rozliczenia = await _rozliczenieRepository.GetAllAsync(ct);
            var podmioty = await _podmiotRepository.GetAllAsync(ct);
            var waluty = await _walutaRepository.GetAllAsync(ct);

            var rozliczeniaLookup = rozliczenia
                .GroupBy(x => x.IdFaktury)
                .ToDictionary(x => x.Key, x => x.Sum(r => r.Kwota));

            var fakturyDoRozliczenia = faktury
                .Select(faktura =>
                {
                    var sumaRozliczen = rozliczeniaLookup.TryGetValue(faktura.IdFaktury, out var suma)
                        ? suma
                        : 0m;

                    var kwotaBrutto = faktura.KwotaBrutto ?? 0m;
                    var kwotaPozostala = kwotaBrutto - sumaRozliczen;

                    return new FakturaDoRozliczeniaDto
                    {
                        IdFaktury = faktura.IdFaktury,
                        NumerFaktury = faktura.NumerFaktury,
                        IdPodmiotu = faktura.IdPodmiotu,
                        DataWystawienia = faktura.DataWystawienia,
                        KodWaluty = faktura.KodWaluty,
                        KwotaBrutto = kwotaBrutto,
                        KwotaRozliczona = sumaRozliczen,
                        KwotaPozostala = kwotaPozostala
                    };
                })
                .Where(x => x.KwotaPozostala > 0)
                .OrderByDescending(x => x.DataWystawienia)
                .ThenBy(x => x.NumerFaktury)
                .ToList();

            return new PlatnoscContextDto
            {
                Podmioty = podmioty.Select(x => _mapper.Map<PodmiotDto>(x)).OrderBy(x => x.Nazwa).ToList(),
                Waluty = waluty.Select(x => _mapper.Map<WalutaDto>(x)).OrderBy(x => x.KodWaluty).ToList(),
                Faktury = fakturyDoRozliczenia
            };
        }
    }
}