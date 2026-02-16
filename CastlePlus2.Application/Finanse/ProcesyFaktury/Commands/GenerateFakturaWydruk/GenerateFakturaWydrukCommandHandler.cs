using System.Globalization;
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Domain.Entities.Finanse;
using CastlePlus2.Domain.Entities.Najem;
using CastlePlus2.Domain.Entities.Podmioty;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.GenerateFakturaWydruk
{
    public class GenerateFakturaWydrukCommandHandler : IRequestHandler<GenerateFakturaWydrukCommand, GenerateFakturaWydrukCommandResult>
    {
        private const string TemplateTag = "[TEMPLATE:FAKTURA]";
        private const string TemplatePrefix = "templates/finanse/faktury/";

        private readonly IFakturaRepository _fakturaRepository;
        private readonly IPozycjaKosztuRepository _pozycjaKosztuRepository;
        private readonly IAlokacjaKosztuRepository _alokacjaKosztuRepository;
        private readonly IKategoriaKosztuRepository _kategoriaKosztuRepository;
        private readonly IPodmiotRepository _podmiotRepository;
        private readonly IKontaktRepository _kontaktRepository;
        private readonly IPrzedmiotNajmuRepository _przedmiotNajmuRepository;
        private readonly IUmowaNajmuRepository _umowaNajmuRepository;
        private readonly ILokalRepository _lokalRepository;
        private readonly IEncjaRepository _encjaRepository;
        private readonly IDokumentRepository _dokumentRepository;
        private readonly IFakturaDocxTemplateRenderer _renderer;
        private readonly IDokumentFileStorage _dokumentFileStorage;

        public GenerateFakturaWydrukCommandHandler(
            IFakturaRepository fakturaRepository,
            IPozycjaKosztuRepository pozycjaKosztuRepository,
            IAlokacjaKosztuRepository alokacjaKosztuRepository,
            IKategoriaKosztuRepository kategoriaKosztuRepository,
            IPodmiotRepository podmiotRepository,
            IKontaktRepository kontaktRepository,
            IPrzedmiotNajmuRepository przedmiotNajmuRepository,
            IUmowaNajmuRepository umowaNajmuRepository,
            ILokalRepository lokalRepository,
            IEncjaRepository encjaRepository,
            IDokumentRepository dokumentRepository,
            IFakturaDocxTemplateRenderer renderer,
            IDokumentFileStorage dokumentFileStorage)
        {
            _fakturaRepository = fakturaRepository;
            _pozycjaKosztuRepository = pozycjaKosztuRepository;
            _alokacjaKosztuRepository = alokacjaKosztuRepository;
            _kategoriaKosztuRepository = kategoriaKosztuRepository;
            _podmiotRepository = podmiotRepository;
            _kontaktRepository = kontaktRepository;
            _przedmiotNajmuRepository = przedmiotNajmuRepository;
            _umowaNajmuRepository = umowaNajmuRepository;
            _lokalRepository = lokalRepository;
            _encjaRepository = encjaRepository;
            _dokumentRepository = dokumentRepository;
            _renderer = renderer;
            _dokumentFileStorage = dokumentFileStorage;
        }

        public async Task<GenerateFakturaWydrukCommandResult> Handle(GenerateFakturaWydrukCommand request, CancellationToken ct)
        {
            var warnings = new List<string>();
            var culture = CultureInfo.GetCultureInfo("pl-PL");

            var faktura = await _fakturaRepository.GetByIdAsync(request.IdFaktury, ct)
                ?? throw new InvalidOperationException($"Nie znaleziono faktury o IdFaktury={request.IdFaktury}.");

            var templateDoc = await _dokumentRepository.GetByIdAsync(request.TemplateDokumentId, ct)
                ?? throw new InvalidOperationException($"Nie znaleziono szablonu o IdDokumentu={request.TemplateDokumentId}.");

            if (!IsTemplateDocument(templateDoc.SciezkaPliku, templateDoc.Opis))
            {
                throw new InvalidOperationException("Wybrany dokument nie spełnia reguł szablonu faktury (ścieżka lub tag). ");
            }

            var templateBytes = await _dokumentFileStorage.ReadAllBytesAsync(templateDoc.SciezkaPliku, ct);

            var pozycje = (await _pozycjaKosztuRepository.GetAllAsync(ct))
                .Where(x => x.IdFaktury == faktura.IdFaktury)
                .OrderBy(x => x.IdPozycjiKosztu)
                .ToList();

            var kategorie = (await _kategoriaKosztuRepository.GetAllAsync(ct))
                .ToDictionary(x => x.IdKategoriiKosztu, x => x);

            var allAlokacje = await _alokacjaKosztuRepository.GetAllAsync(ct);
            var alokacjeByPozycja = allAlokacje
                .Where(x => pozycje.Any(p => p.IdPozycjiKosztu == x.IdPozycjiKosztu))
                .GroupBy(x => x.IdPozycjiKosztu)
                .ToDictionary(g => g.Key, g => g.ToList());

            var nabywca = await _podmiotRepository.GetByIdAsync(faktura.IdPodmiotu, ct)
                ?? throw new InvalidOperationException($"Nie znaleziono podmiotu nabywcy o IdPodmiotu={faktura.IdPodmiotu}.");
            var nabywcaKontakty = await _kontaktRepository.GetByPodmiotIdAsync(nabywca.IdPodmiotu, ct);

            var seller = await InferSellerAsync(faktura, allAlokacje, warnings, ct);
            var sellerKontakty = seller is null
                ? new List<Kontakt>()
                : await _kontaktRepository.GetByPodmiotIdAsync(seller.IdPodmiotu, ct);

            var lokale = await _lokalRepository.GetAllAsync(ct);
            var lokaleById = lokale.ToDictionary(x => x.Id, x => x);
            var encjeById = (await _encjaRepository.GetAllAsync(ct)).ToDictionary(x => x.Id, x => x);

            var placeholders = BuildPlaceholders(faktura, nabywca, nabywcaKontakty, seller, sellerKontakty, warnings, culture);
            var tableRows = BuildRows(pozycje, kategorie, alokacjeByPozycja, lokaleById, encjeById, request.IncludeAllocations, culture);

            var bytes = _renderer.Render(templateBytes, placeholders, tableRows);
            var safeNumber = SanitizeFileNamePart(faktura.NumerFaktury);

            return new GenerateFakturaWydrukCommandResult
            {
                Bytes = bytes,
                FileName = $"Faktura_{safeNumber}.docx",
                Warnings = warnings
            };
        }

        private async Task<Podmiot?> InferSellerAsync(Faktura faktura, List<AlokacjaKosztu> allAlokacje, List<string> warnings, CancellationToken ct)
        {
            var pozycje = (await _pozycjaKosztuRepository.GetAllAsync(ct)).Where(x => x.IdFaktury == faktura.IdFaktury).ToList();
            if (pozycje.Count == 0)
            {
                warnings.Add("Faktura nie ma pozycji kosztu - pominięto wyznaczanie sprzedawcy.");
                return null;
            }

            var pozycjeIds = pozycje.Select(x => x.IdPozycjiKosztu).ToHashSet();
            var entityIds = allAlokacje
                .Where(x => pozycjeIds.Contains(x.IdPozycjiKosztu))
                .Select(x => x.IdEncji)
                .Distinct()
                .ToHashSet();

            if (entityIds.Count == 0)
            {
                warnings.Add("Brak alokacji kosztów - nie wyznaczono sprzedawcy z umowy najmu.");
                return null;
            }

            var invoiceDate = (faktura.DataSprzedazy ?? faktura.DataWystawienia).Date;
            var monthStart = new DateOnly(invoiceDate.Year, invoiceDate.Month, 1);
            var monthEnd = new DateOnly(invoiceDate.Year, invoiceDate.Month, DateTime.DaysInMonth(invoiceDate.Year, invoiceDate.Month));

            var przedmioty = (await _przedmiotNajmuRepository.GetAllAsync(ct))
                .Where(x => entityIds.Contains(x.IdEncji)
                            && x.OdDnia <= monthEnd
                            && (x.DoDnia == null || x.DoDnia >= monthStart))
                .ToList();

            if (przedmioty.Count == 0)
            {
                warnings.Add("Nie znaleziono aktywnych przedmiotów najmu dla alokacji faktury i miesiąca faktury.");
                return null;
            }

            var umowy = (await _umowaNajmuRepository.GetAllAsync(ct))
                .Where(x => przedmioty.Any(p => p.IdUmowyNajmu == x.Id)
                            && x.IdNajemcy == faktura.IdPodmiotu
                            && x.DataPoczatku.Date <= monthEnd.ToDateTime(TimeOnly.MinValue)
                            && (x.DataZakonczenia == null || x.DataZakonczenia.Value.Date >= monthStart.ToDateTime(TimeOnly.MinValue)))
                .ToList();

            if (umowy.Count == 0)
            {
                warnings.Add("Nie znaleziono umowy najmu dla nabywcy i alokacji kosztów. Pola sprzedawcy pozostawiono puste.");
                return null;
            }

            UmowaNajmu chosen;
            if (umowy.Count == 1)
            {
                chosen = umowy[0];
            }
            else
            {
                chosen = umowy
                    .OrderByDescending(u => przedmioty.Count(p => p.IdUmowyNajmu == u.Id))
                    .ThenByDescending(u => u.DataPoczatku)
                    .First();

                warnings.Add("Znaleziono wiele umów najmu. Wybrano umowę z największą liczbą pasujących przedmiotów najmu, a następnie najnowszą datą początku.");
            }

            return await _podmiotRepository.GetByIdAsync(chosen.IdWynajmujacego, ct);
        }

        private static Dictionary<string, string> BuildPlaceholders(
            Faktura faktura,
            Podmiot nabywca,
            List<Kontakt> nabywcaKontakty,
            Podmiot? seller,
            List<Kontakt> sellerKontakty,
            List<string> warnings,
            CultureInfo culture)
        {
            string FindContact(List<Kontakt> contacts, string marker)
                => contacts.FirstOrDefault(x => x.Rodzaj.Contains(marker, StringComparison.OrdinalIgnoreCase))?.Wartosc ?? string.Empty;

            var map = new Dictionary<string, string>
            {
                ["NUMER_FAKTURY"] = faktura.NumerFaktury,
                ["DATA_WYSTAWIENIA"] = faktura.DataWystawienia.ToString("yyyy-MM-dd", culture),
                ["DATA_SPRZEDAZY"] = faktura.DataSprzedazy?.ToString("yyyy-MM-dd", culture) ?? string.Empty,
                ["KOD_WALUTY"] = faktura.KodWaluty,
                ["KWOTA_NETTO"] = (faktura.KwotaNetto ?? 0m).ToString("0.00", culture),
                ["KWOTA_BRUTTO"] = (faktura.KwotaBrutto ?? 0m).ToString("0.00", culture),

                ["NABYWCA_NAZWA"] = nabywca.Nazwa,
                ["NABYWCA_NIP"] = nabywca.NIP ?? string.Empty,
                ["NABYWCA_REGON"] = nabywca.REGON ?? string.Empty,
                ["NABYWCA_PESEL"] = nabywca.PESEL ?? string.Empty,
                ["NABYWCA_TYP"] = nabywca.TypPodmiotu,
                ["NABYWCA_EMAIL"] = FindContact(nabywcaKontakty, "email"),
                ["NABYWCA_TELEFON"] = FindContact(nabywcaKontakty, "telefon"),
                ["NABYWCA_RACHUNEK"] = FindContact(nabywcaKontakty, "rachunek"),

                ["SPRZEDAWCA_NAZWA"] = seller?.Nazwa ?? string.Empty,
                ["SPRZEDAWCA_NIP"] = seller?.NIP ?? string.Empty,
                ["SPRZEDAWCA_REGON"] = seller?.REGON ?? string.Empty,
                ["SPRZEDAWCA_PESEL"] = seller?.PESEL ?? string.Empty,
                ["SPRZEDAWCA_TYP"] = seller?.TypPodmiotu ?? string.Empty,
                ["SPRZEDAWCA_EMAIL"] = FindContact(sellerKontakty, "email"),
                ["SPRZEDAWCA_TELEFON"] = FindContact(sellerKontakty, "telefon"),
                ["SPRZEDAWCA_RACHUNEK"] = FindContact(sellerKontakty, "rachunek"),
                ["WARNINGS"] = string.Join(" | ", warnings)
            };

            return map;
        }

        private static List<IReadOnlyDictionary<string, string>> BuildRows(
            List<PozycjaKosztu> pozycje,
            Dictionary<long, KategoriaKosztu> kategorie,
            Dictionary<long, List<AlokacjaKosztu>> alokacjeByPozycja,
            Dictionary<Guid, Lokal> lokaleById,
            Dictionary<Guid, Encja> encjeById,
            bool includeAllocations,
            CultureInfo culture)
        {
            var rows = new List<IReadOnlyDictionary<string, string>>();

            for (var i = 0; i < pozycje.Count; i++)
            {
                var pozycja = pozycje[i];
                kategorie.TryGetValue(pozycja.IdKategoriiKosztu, out var kategoria);
                var alokacje = alokacjeByPozycja.TryGetValue(pozycja.IdPozycjiKosztu, out var value)
                    ? value
                    : new List<AlokacjaKosztu>();

                var allocationsText = string.Empty;
                if (includeAllocations && alokacje.Count > 0)
                {
                    var names = alokacje
                        .Select(x => lokaleById.TryGetValue(x.IdEncji, out var lokal)
                            ? lokal.KodLokalu
                            : encjeById.TryGetValue(x.IdEncji, out var encja)
                                ? (encja.KodEncji ?? encja.Id.ToString())
                                : x.IdEncji.ToString())
                        .Distinct()
                        .ToList();

                    allocationsText = string.Join(", ", names);
                }

                rows.Add(new Dictionary<string, string>
                {
                    ["LP"] = (i + 1).ToString(culture),
                    ["KATEGORIA"] = kategoria?.Nazwa ?? string.Empty,
                    ["KATEGORIA_KOD"] = kategoria?.Kod ?? string.Empty,
                    ["OPIS"] = pozycja.Opis ?? string.Empty,
                    ["NETTO"] = pozycja.KwotaNetto.ToString("0.00", culture),
                    ["BRUTTO"] = pozycja.KwotaBrutto.ToString("0.00", culture),
                    ["ALOKACJE"] = allocationsText
                });
            }

            return rows;
        }

        private static bool IsTemplateDocument(string path, string? description)
        {
            return path.StartsWith(TemplatePrefix, StringComparison.OrdinalIgnoreCase)
                   || (!string.IsNullOrWhiteSpace(description) && description.Contains(TemplateTag, StringComparison.OrdinalIgnoreCase));
        }

        private static string SanitizeFileNamePart(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "BrakNumeru";

            var invalid = Path.GetInvalidFileNameChars().ToHashSet();
            var chars = input.Select(ch => invalid.Contains(ch) ? '_' : ch).ToArray();
            return new string(chars);
        }
    }
}