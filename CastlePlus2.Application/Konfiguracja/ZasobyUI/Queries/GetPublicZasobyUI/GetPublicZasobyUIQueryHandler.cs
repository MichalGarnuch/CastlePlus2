using AutoMapper;
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Application.Interfaces.Konfiguracja;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Domain.Entities.Konfiguracja;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUI.Queries.GetPublicZasobyUI
{
    public class GetPublicZasobyUIQueryHandler : IRequestHandler<GetPublicZasobyUIQuery, List<ZasobUIPublicDto>>
    {
        private readonly IZasobUIRepository _repo;
        private readonly IPowiazanieDokumentuRepository _powiazanieRepo;
        private readonly IMapper _mapper;

        public GetPublicZasobyUIQueryHandler(
            IZasobUIRepository repo,
            IPowiazanieDokumentuRepository powiazanieRepo,
            IMapper mapper)
        {
            _repo = repo;
            _powiazanieRepo = powiazanieRepo;
            _mapper = mapper;
        }

        public async Task<List<ZasobUIPublicDto>> Handle(GetPublicZasobyUIQuery request, CancellationToken ct)
        {
            var nowUtc = DateTime.UtcNow;
            var zasoby = await _repo.GetPublicAsync(request.Typ, request.Kategoria, request.IncludeInactive, nowUtc, ct);

            var encje = zasoby.Select(x => x.IdEncji).ToList();
            var dokumentyMap = await _powiazanieRepo.GetDokumentyByEncjeIdsAsync(encje, ct);

            var result = new List<ZasobUIPublicDto>();
            foreach (var zasob in zasoby)
            {
                var teksty = WybierzTeksty(zasob, request.Jezyk);
                var dokumenty = dokumentyMap.TryGetValue(zasob.IdEncji, out var list)
                    ? list
                    : new List<CastlePlus2.Domain.Entities.Dokumenty.Dokument>();

                result.Add(new ZasobUIPublicDto
                {
                    IdEncji = zasob.IdEncji,
                    KodZasobu = zasob.KodZasobu,
                    Typ = zasob.Typ,
                    Kategoria = zasob.Kategoria,
                    Sort = zasob.Sort,
                    Teksty = teksty.Select(x => _mapper.Map<ZasobUITekstDto>(x)).ToList(),
                    Dokumenty = dokumenty.Select(x => new ZasobUIDokumentDto
                    {
                        IdDokumentu = x.IdDokumentu,
                        Nazwa = x.Nazwa,
                        Opis = x.Opis,
                        SciezkaPliku = x.SciezkaPliku
                    }).ToList()
                });
            }

            return result;
        }

        private static IEnumerable<ZasobUITekst> WybierzTeksty(ZasobUI zasob, string jezyk)
        {
            var wybrane = zasob.Teksty.Where(x => x.Jezyk == jezyk).ToList();
            if (wybrane.Count == 0 && !string.Equals(jezyk, "pl-PL", StringComparison.OrdinalIgnoreCase))
            {
                wybrane = zasob.Teksty.Where(x => x.Jezyk == "pl-PL").ToList();
            }

            return wybrane;
        }
    }
}