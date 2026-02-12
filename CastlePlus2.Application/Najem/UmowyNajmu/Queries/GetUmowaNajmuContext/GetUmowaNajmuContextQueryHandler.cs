using AutoMapper;
using System.Linq;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Queries.GetUmowaNajmuContext
{
    public class GetUmowaNajmuContextQueryHandler : IRequestHandler<GetUmowaNajmuContextQuery, UmowaNajmuContextDto>
    {
        private readonly IWalutaRepository _walutaRepository;
        private readonly IIndeksacjaRepository _indeksacjaRepository;
        private readonly IJednostkaMiaryRepository _jednostkaRepository;
        private readonly IMapper _mapper;

        public GetUmowaNajmuContextQueryHandler(
            IWalutaRepository walutaRepository,
            IIndeksacjaRepository indeksacjaRepository,
            IJednostkaMiaryRepository jednostkaRepository,
            IMapper mapper)
        {
            _walutaRepository = walutaRepository;
            _indeksacjaRepository = indeksacjaRepository;
            _jednostkaRepository = jednostkaRepository;
            _mapper = mapper;
        }

        public async Task<UmowaNajmuContextDto> Handle(GetUmowaNajmuContextQuery request, CancellationToken ct)
        {
            var waluty = await _walutaRepository.GetAllAsync(ct);
            var indeksacje = await _indeksacjaRepository.GetAllAsync(ct);
            var jednostki = await _jednostkaRepository.GetAllAsync(ct);

            return new UmowaNajmuContextDto
            {
                Lokale = new List<LokalDto>(),
                Podmioty = new List<PodmiotDto>(),
                Waluty = waluty.Select(x => _mapper.Map<WalutaDto>(x)).OrderBy(x => x.KodWaluty).ToList(),
                Indeksacje = indeksacje.Select(x => _mapper.Map<IndeksacjaDto>(x)).OrderBy(x => x.KodIndeksacji).ToList(),
                JednostkiMiary = jednostki.Select(x => _mapper.Map<JednostkaMiaryDto>(x)).OrderBy(x => x.KodJednostki).ToList()
            };
        }
    }
}