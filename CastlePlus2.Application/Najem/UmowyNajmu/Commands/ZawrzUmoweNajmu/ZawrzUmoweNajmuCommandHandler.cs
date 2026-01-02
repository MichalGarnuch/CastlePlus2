using CastlePlus2.Application.Common.Exceptions;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Domain.Entities.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Commands.ZawrzUmoweNajmu
{
    public class ZawrzUmoweNajmuCommandHandler : IRequestHandler<ZawrzUmoweNajmuCommand, ZawrzUmoweNajmuResult>
    {
        private const string TypEncjiUmowy = "UMOWA_NAJMU";
        private const string RodzajOperacjiWplata = "WPLATA";

        private readonly ILokalRepository _lokalRepository;
        private readonly IUmowaNajmuRepository _umowaRepository;
        private readonly IPrzedmiotNajmuRepository _przedmiotRepository;
        private readonly ISkladnikCzynszuRepository _skladnikRepository;
        private readonly IKaucjaRepository _kaucjaRepository;
        private readonly IUmowaNajmuKodGenerator _kodGenerator;

        public ZawrzUmoweNajmuCommandHandler(
            ILokalRepository lokalRepository,
            IUmowaNajmuRepository umowaRepository,
            IPrzedmiotNajmuRepository przedmiotRepository,
            ISkladnikCzynszuRepository skladnikRepository,
            IKaucjaRepository kaucjaRepository,
            IUmowaNajmuKodGenerator kodGenerator)
        {
            _lokalRepository = lokalRepository;
            _umowaRepository = umowaRepository;
            _przedmiotRepository = przedmiotRepository;
            _skladnikRepository = skladnikRepository;
            _kaucjaRepository = kaucjaRepository;
            _kodGenerator = kodGenerator;
        }

        public async Task<ZawrzUmoweNajmuResult> Handle(ZawrzUmoweNajmuCommand request, CancellationToken ct)
        {
            var lokal = await _lokalRepository.GetByIdAsync(request.IdLokalu, ct);
            if (lokal is null)
                throw new InvalidOperationException("Nie znaleziono lokalu o podanym IdLokalu.");

            var overlapExists = await _przedmiotRepository.ExistsOverlapAsync(
                request.IdLokalu,
                request.DataPoczatku,
                request.DataZakonczenia,
                ct);

            if (overlapExists)
                throw new BusinessConflictException("Istnieje już umowa najmu dla tego lokalu w podanym okresie.");

            var encjaId = Guid.NewGuid();
            var nowUtc = DateTime.UtcNow;
            var kodEncji = string.IsNullOrWhiteSpace(request.KodEncji)
                ? await _kodGenerator.GenerateUmowaNajmuKodAsync(request.DataZawarcia, ct)
                : request.KodEncji.Trim();
            var udzialProcent = request.UdzialProcent ?? 100.0000m;

            var umowa = new UmowaNajmu
            {
                Id = encjaId,
                TypEncji = TypEncjiUmowy,
                KodEncji = kodEncji,
                UtworzonoUtc = nowUtc,
                ZmienionoUtc = nowUtc,

                IdWynajmujacego = request.IdWynajmujacego,
                IdNajemcy = request.IdNajemcy,
                DataZawarcia = request.DataZawarcia.ToDateTime(TimeOnly.MinValue),
                DataPoczatku = request.DataPoczatku.ToDateTime(TimeOnly.MinValue),
                DataZakonczenia = request.DataZakonczenia?.ToDateTime(TimeOnly.MinValue),
                KodWaluty = request.KodWaluty,
                KodIndeksacji = request.KodIndeksacji
            };

            var przedmiot = new PrzedmiotNajmu
            {
                IdUmowyNajmu = encjaId,
                IdEncji = lokal.Id,
                UdzialProcent = udzialProcent,
                OdDnia = request.DataPoczatku,
                DoDnia = request.DataZakonczenia
            };

            var skladnik = new SkladnikCzynszu
            {
                IdUmowyNajmu = encjaId,
                Nazwa = request.NazwaCzynszu,
                KodJednostki = request.KodJednostki,
                Stawka = request.Stawka,
                IloscBazowa = request.IloscBazowa,
                KodIndeksacji = request.KodIndeksacji,
                OdDnia = request.DataPoczatku,
                DoDnia = request.DataZakonczenia
            };

            // Dodajemy wszystko do TEGO SAMEGO kontekstu (przez repozytoria)
            await _umowaRepository.AddAsync(umowa, ct);
            await _przedmiotRepository.AddAsync(przedmiot, ct);
            await _skladnikRepository.AddAsync(skladnik, ct);

            if (request.KwotaKaucji.GetValueOrDefault() > 0)
            {
                var kaucja = new Kaucja
                {
                    IdUmowyNajmu = encjaId,
                    RodzajOperacji = RodzajOperacjiWplata,
                    Kwota = request.KwotaKaucji!.Value,
                    KodWaluty = request.KodWaluty,
                    DataOperacji = request.DataZawarcia
                };

                await _kaucjaRepository.AddAsync(kaucja, ct);
            }

            // JEDEN zapis na końcu (to jest “porządnie”)
            await _umowaRepository.SaveChangesAsync(ct);

            return new ZawrzUmoweNajmuResult { IdEncjiUmowy = encjaId };
        }
    }
}
