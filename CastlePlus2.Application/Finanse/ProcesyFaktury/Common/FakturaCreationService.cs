using CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.WystawFakture;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Domain.Entities.Finanse;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Common
{
    public class FakturaCreationService : IFakturaCreationService
    {
        private readonly IFakturaRepository _fakturaRepository;
        private readonly IPodmiotRepository _podmiotRepository;
        private readonly IWalutaRepository _walutaRepository;
        private readonly IKategoriaKosztuRepository _kategoriaRepository;
        private readonly IEncjaRepository _encjaRepository;

        public FakturaCreationService(
            IFakturaRepository fakturaRepository,
            IPodmiotRepository podmiotRepository,
            IWalutaRepository walutaRepository,
            IKategoriaKosztuRepository kategoriaRepository,
            IEncjaRepository encjaRepository)
        {
            _fakturaRepository = fakturaRepository;
            _podmiotRepository = podmiotRepository;
            _walutaRepository = walutaRepository;
            _kategoriaRepository = kategoriaRepository;
            _encjaRepository = encjaRepository;
        }

        public async Task<WystawFaktureResultDto> CreateAsync(WystawFaktureCommand request, CancellationToken ct)
        {
            request.NumerFaktury = request.NumerFaktury?.Trim() ?? string.Empty;
            request.KodWaluty = request.KodWaluty?.Trim().ToUpperInvariant() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(request.NumerFaktury))
                throw new InvalidOperationException("Numer faktury jest wymagany.");

            if (request.NumerFaktury.Length > 60)
                throw new InvalidOperationException("Numer faktury może mieć maksymalnie 60 znaków.");

            if (request.IdPodmiotu <= 0)
                throw new InvalidOperationException("IdPodmiotu musi być > 0.");

            if (request.KodWaluty.Length != 3)
                throw new InvalidOperationException("KodWaluty musi mieć dokładnie 3 znaki.");

            if (request.Pozycje.Count == 0)
                throw new InvalidOperationException("Faktura musi zawierać przynajmniej jedną pozycję kosztu.");

            if (await _fakturaRepository.ExistsByNumerAsync(request.NumerFaktury, ct))
                throw new InvalidOperationException("Faktura o podanym numerze już istnieje.");

            var podmiot = await _podmiotRepository.GetByIdAsync(request.IdPodmiotu, ct);
            if (podmiot is null)
                throw new InvalidOperationException("Nie znaleziono podmiotu dla IdPodmiotu.");

            var waluta = await _walutaRepository.GetByKodAsync(request.KodWaluty, ct);
            if (waluta is null)
                throw new InvalidOperationException("Nie znaleziono waluty dla podanego KodWaluty.");

            var kategorieSprawdzone = new HashSet<long>();
            var encjeSprawdzone = new HashSet<Guid>();

            foreach (var pozycja in request.Pozycje)
            {
                if (!kategorieSprawdzone.Contains(pozycja.IdKategoriiKosztu))
                {
                    var kategoria = await _kategoriaRepository.GetByIdAsync(pozycja.IdKategoriiKosztu, ct);
                    if (kategoria is null)
                        throw new InvalidOperationException($"Nie znaleziono kategorii kosztu: Id={pozycja.IdKategoriiKosztu}.");

                    kategorieSprawdzone.Add(pozycja.IdKategoriiKosztu);
                }

                if (pozycja.Alokacje.Count == 0)
                    throw new InvalidOperationException("Pozycja kosztu musi mieć przynajmniej jedną alokację.");

                var sumaNetto = pozycja.Alokacje.Sum(x => x.KwotaNetto);
                var sumaBrutto = pozycja.Alokacje.Sum(x => x.KwotaBrutto);

                if (sumaNetto != pozycja.KwotaNetto || sumaBrutto != pozycja.KwotaBrutto)
                {
                    throw new InvalidOperationException("Suma alokacji musi być równa kwotom pozycji (netto i brutto).");
                }

                foreach (var alokacja in pozycja.Alokacje)
                {
                    if (encjeSprawdzone.Contains(alokacja.IdEncji))
                        continue;

                    var encja = await _encjaRepository.GetByIdAsync(alokacja.IdEncji, ct);
                    if (encja is null)
                        throw new InvalidOperationException($"Nie znaleziono encji: Id={alokacja.IdEncji}.");

                    encjeSprawdzone.Add(alokacja.IdEncji);
                }
            }

            var sumaFakturyNetto = request.Pozycje.Sum(x => x.KwotaNetto);
            var sumaFakturyBrutto = request.Pozycje.Sum(x => x.KwotaBrutto);

            var faktura = new Faktura
            {
                NumerFaktury = request.NumerFaktury,
                IdPodmiotu = request.IdPodmiotu,
                DataWystawienia = request.DataWystawienia.Date,
                DataSprzedazy = request.DataSprzedazy?.Date,
                KodWaluty = request.KodWaluty,
                KwotaNetto = sumaFakturyNetto,
                KwotaBrutto = sumaFakturyBrutto
            };

            foreach (var pozycja in request.Pozycje)
            {
                var pozycjaEntity = new PozycjaKosztu
                {
                    IdKategoriiKosztu = pozycja.IdKategoriiKosztu,
                    Opis = string.IsNullOrWhiteSpace(pozycja.Opis) ? null : pozycja.Opis.Trim(),
                    KwotaNetto = pozycja.KwotaNetto,
                    KwotaBrutto = pozycja.KwotaBrutto,
                    Faktura = faktura
                };

                foreach (var alokacja in pozycja.Alokacje)
                {
                    pozycjaEntity.AlokacjeKosztu.Add(new AlokacjaKosztu
                    {
                        IdEncji = alokacja.IdEncji,
                        KwotaNetto = alokacja.KwotaNetto,
                        KwotaBrutto = alokacja.KwotaBrutto,
                        PozycjaKosztu = pozycjaEntity
                    });
                }

                faktura.PozycjeKosztu.Add(pozycjaEntity);
            }

            await _fakturaRepository.AddAsync(faktura, ct);
            await _fakturaRepository.SaveChangesAsync(ct);

            return new WystawFaktureResultDto
            {
                IdFaktury = faktura.IdFaktury,
                KwotaNetto = sumaFakturyNetto,
                KwotaBrutto = sumaFakturyBrutto
            };
        }
    }
}