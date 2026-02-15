using CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.WystawFakture;
using CastlePlus2.Application.Finanse.ProcesyFaktury.Common;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.GenerateNajemFaktury
{
    public class GenerateNajemFakturyCommandHandler : IRequestHandler<GenerateNajemFakturyCommand, GenerateNajemFakturyResultDto>
    {
        private const string DefaultCzynszCategoryCode = "CZYNSZ";

        private readonly IUmowaNajmuRepository _umowaNajmuRepository;
        private readonly ISkladnikCzynszuRepository _skladnikCzynszuRepository;
        private readonly IPrzedmiotNajmuRepository _przedmiotNajmuRepository;
        private readonly IFakturaRepository _fakturaRepository;
        private readonly IKategoriaKosztuRepository _kategoriaKosztuRepository;
        private readonly IFakturaCreationService _fakturaCreationService;

        public GenerateNajemFakturyCommandHandler(
            IUmowaNajmuRepository umowaNajmuRepository,
            ISkladnikCzynszuRepository skladnikCzynszuRepository,
            IPrzedmiotNajmuRepository przedmiotNajmuRepository,
            IFakturaRepository fakturaRepository,
            IKategoriaKosztuRepository kategoriaKosztuRepository,
            IFakturaCreationService fakturaCreationService)
        {
            _umowaNajmuRepository = umowaNajmuRepository;
            _skladnikCzynszuRepository = skladnikCzynszuRepository;
            _przedmiotNajmuRepository = przedmiotNajmuRepository;
            _fakturaRepository = fakturaRepository;
            _kategoriaKosztuRepository = kategoriaKosztuRepository;
            _fakturaCreationService = fakturaCreationService;
        }

        public async Task<GenerateNajemFakturyResultDto> Handle(GenerateNajemFakturyCommand request, CancellationToken ct)
        {
            var (monthStart, monthEnd) = ParseMonthRange(request.Miesiac);
            var result = new GenerateNajemFakturyResultDto
            {
                Miesiac = request.Miesiac,
                DataWystawienia = request.DataWystawienia.Date
            };

            var kategoriaCzynszu = await _kategoriaKosztuRepository.GetByKodAsync(DefaultCzynszCategoryCode, ct);
            if (kategoriaCzynszu is null)
            {
                throw new InvalidOperationException($"Nie znaleziono kategorii kosztu o kodzie '{DefaultCzynszCategoryCode}'.");
            }

            var umowy = await _umowaNajmuRepository.GetActiveInRangeAsync(monthStart, monthEnd, ct);
            foreach (var umowa in umowy)
            {
                var item = new GenerateNajemFakturyItemDto
                {
                    IdUmowyNajmu = umowa.Id,
                    IdNajemcy = umowa.IdNajemcy,
                    NumerFaktury = BuildDeterministicInvoiceNumber(request.Miesiac, umowa.Id)
                };

                result.Items.Add(item);

                if (await _fakturaRepository.ExistsByNumerAsync(item.NumerFaktury, ct))
                {
                    item.Status = "Skipped";
                    item.Warnings.Add("already exists");
                    continue;
                }

                try
                {
                    var skladniki = await _skladnikCzynszuRepository.GetActiveInRangeByUmowaIdAsync(
                        umowa.Id,
                        DateOnly.FromDateTime(monthStart),
                        DateOnly.FromDateTime(monthEnd),
                        ct);

                    if (skladniki.Count == 0)
                    {
                        item.Status = "Skipped";
                        item.Warnings.Add("Brak aktywnych składników czynszu.");
                        continue;
                    }

                    var przedmioty = await _przedmiotNajmuRepository.GetActiveInRangeByUmowaIdAsync(
                        umowa.Id,
                        DateOnly.FromDateTime(monthStart),
                        DateOnly.FromDateTime(monthEnd),
                        ct);

                    if (przedmioty.Count == 0)
                    {
                        item.Status = "Error";
                        item.Error = "Brak aktywnych przedmiotów najmu do alokacji kosztu.";
                        continue;
                    }

                    var pozycje = new List<WystawFakturePozycjaCommand>();
                    foreach (var skladnik in skladniki)
                    {
                        var baseQty = skladnik.IloscBazowa ?? 1m;
                        if (skladnik.IloscBazowa is null)
                        {
                            item.Warnings.Add($"Składnik '{skladnik.Nazwa}' ma IloscBazowa = NULL. Użyto 1.");
                        }

                        var amount = decimal.Round(skladnik.Stawka * baseQty, 2, MidpointRounding.AwayFromZero);
                        var alokacje = BuildAllocations(przedmioty, amount);

                        pozycje.Add(new WystawFakturePozycjaCommand
                        {
                            IdKategoriiKosztu = kategoriaCzynszu.IdKategoriiKosztu,
                            Opis = skladnik.Nazwa,
                            KwotaNetto = amount,
                            KwotaBrutto = amount,
                            Alokacje = alokacje
                        });
                    }

                    var createResult = await _fakturaCreationService.CreateAsync(new WystawFaktureCommand
                    {
                        NumerFaktury = item.NumerFaktury,
                        IdPodmiotu = umowa.IdNajemcy,
                        DataWystawienia = request.DataWystawienia,
                        DataSprzedazy = monthEnd,
                        KodWaluty = umowa.KodWaluty,
                        Pozycje = pozycje
                    }, ct);

                    item.KwotaNetto = createResult.KwotaNetto;
                    item.KwotaBrutto = createResult.KwotaBrutto;
                    item.Status = "Created";
                }
                catch (Exception ex)
                {
                    item.Status = "Error";
                    item.Error = ex.Message;
                }
            }

            return result;
        }

        private static (DateTime MonthStart, DateTime MonthEnd) ParseMonthRange(string month)
        {
            var year = int.Parse(month[..4]);
            var monthNumber = int.Parse(month.Substring(5, 2));
            var firstDay = new DateTime(year, monthNumber, 1);
            return (firstDay, firstDay.AddMonths(1).AddDays(-1));
        }

        private static string BuildDeterministicInvoiceNumber(string month, Guid idUmowyNajmu)
        {
            var compact = month.Replace("-", string.Empty);
            var shortGuid = idUmowyNajmu.ToString("N")[..12].ToUpperInvariant();
            return $"NJM-{compact}-{shortGuid}";
        }

        private static List<WystawFaktureAlokacjaCommand> BuildAllocations(List<Domain.Entities.Najem.PrzedmiotNajmu> przedmioty, decimal totalAmount)
        {
            var allocations = new List<WystawFaktureAlokacjaCommand>();
            var withShares = przedmioty.Where(x => x.UdzialProcent.HasValue).ToList();

            if (withShares.Count == przedmioty.Count)
            {
                var shareSum = withShares.Sum(x => x.UdzialProcent!.Value);
                if (shareSum <= 0)
                {
                    return BuildEqualAllocations(przedmioty, totalAmount);
                }

                decimal running = 0m;
                for (var i = 0; i < withShares.Count; i++)
                {
                    var subject = withShares[i];
                    var amount = i == withShares.Count - 1
                        ? decimal.Round(totalAmount - running, 2, MidpointRounding.AwayFromZero)
                        : decimal.Round(totalAmount * (subject.UdzialProcent!.Value / shareSum), 2, MidpointRounding.AwayFromZero);

                    running += amount;
                    allocations.Add(new WystawFaktureAlokacjaCommand
                    {
                        IdEncji = subject.IdEncji,
                        KwotaNetto = amount,
                        KwotaBrutto = amount
                    });
                }

                return allocations;
            }

            return BuildEqualAllocations(przedmioty, totalAmount);
        }

        private static List<WystawFaktureAlokacjaCommand> BuildEqualAllocations(List<Domain.Entities.Najem.PrzedmiotNajmu> przedmioty, decimal totalAmount)
        {
            var allocations = new List<WystawFaktureAlokacjaCommand>();
            decimal running = 0m;
            for (var i = 0; i < przedmioty.Count; i++)
            {
                var subject = przedmioty[i];
                var amount = i == przedmioty.Count - 1
                    ? decimal.Round(totalAmount - running, 2, MidpointRounding.AwayFromZero)
                    : decimal.Round(totalAmount / przedmioty.Count, 2, MidpointRounding.AwayFromZero);

                running += amount;
                allocations.Add(new WystawFaktureAlokacjaCommand
                {
                    IdEncji = subject.IdEncji,
                    KwotaNetto = amount,
                    KwotaBrutto = amount
                });
            }

            return allocations;
        }
    }
}