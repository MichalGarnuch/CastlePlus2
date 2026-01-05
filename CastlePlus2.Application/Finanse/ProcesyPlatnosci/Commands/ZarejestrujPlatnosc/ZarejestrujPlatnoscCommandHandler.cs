using System;
using System.Collections.Generic;
using System.Linq;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.ProcesyPlatnosci.Commands.ZarejestrujPlatnosc
{
    public class ZarejestrujPlatnoscCommandHandler : IRequestHandler<ZarejestrujPlatnoscCommand, ZarejestrujPlatnoscResultDto>
    {
        private readonly IPlatnoscRepository _platnoscRepository;
        private readonly IRozliczeniePlatnosciRepository _rozliczenieRepository;
        private readonly IFakturaRepository _fakturaRepository;
        private readonly IPodmiotRepository _podmiotRepository;
        private readonly IWalutaRepository _walutaRepository;

        public ZarejestrujPlatnoscCommandHandler(
            IPlatnoscRepository platnoscRepository,
            IRozliczeniePlatnosciRepository rozliczenieRepository,
            IFakturaRepository fakturaRepository,
            IPodmiotRepository podmiotRepository,
            IWalutaRepository walutaRepository)
        {
            _platnoscRepository = platnoscRepository;
            _rozliczenieRepository = rozliczenieRepository;
            _fakturaRepository = fakturaRepository;
            _podmiotRepository = podmiotRepository;
            _walutaRepository = walutaRepository;
        }

        public async Task<ZarejestrujPlatnoscResultDto> Handle(ZarejestrujPlatnoscCommand request, CancellationToken ct)
        {
            request.KodWaluty = request.KodWaluty?.Trim().ToUpperInvariant() ?? string.Empty;

            if (request.IdPodmiotu <= 0)
                throw new InvalidOperationException("IdPodmiotu musi być > 0.");

            if (request.DataPlatnosci == default)
                throw new InvalidOperationException("Data płatności jest wymagana.");

            if (string.IsNullOrWhiteSpace(request.KodWaluty) || request.KodWaluty.Length != 3)
                throw new InvalidOperationException("KodWaluty musi mieć dokładnie 3 znaki.");

            if (request.Kwota <= 0)
                throw new InvalidOperationException("Kwota płatności musi być większa od zera.");

            if (request.Rozliczenia.Count == 0)
                throw new InvalidOperationException("Płatność musi zawierać przynajmniej jedno rozliczenie.");

            var sumaRozliczen = request.Rozliczenia.Sum(x => x.Kwota);
            if (sumaRozliczen > request.Kwota)
                throw new InvalidOperationException("Suma rozliczeń nie może przekraczać kwoty płatności.");

            var podmiot = await _podmiotRepository.GetByIdAsync(request.IdPodmiotu, ct);
            if (podmiot is null)
                throw new InvalidOperationException("Nie znaleziono podmiotu dla IdPodmiotu.");

            var waluta = await _walutaRepository.GetByKodAsync(request.KodWaluty, ct);
            if (waluta is null)
                throw new InvalidOperationException("Nie znaleziono waluty dla podanego KodWaluty.");

            var fakturaIds = request.Rozliczenia.Select(x => x.IdFaktury).Distinct().ToList();
            var rozliczeniaIstniejace = await _rozliczenieRepository.GetAllAsync(ct);
            var rozliczeniaLookup = rozliczeniaIstniejace
                .Where(x => fakturaIds.Contains(x.IdFaktury))
                .GroupBy(x => x.IdFaktury)
                .ToDictionary(x => x.Key, x => x.Sum(r => r.Kwota));

            var faktury = new Dictionary<long, Faktura>();
            foreach (var idFaktury in fakturaIds)
            {
                var faktura = await _fakturaRepository.GetForUpdateAsync(idFaktury, ct);
                if (faktura is null)
                    throw new InvalidOperationException($"Nie znaleziono faktury: Id={idFaktury}.");

                if (!string.Equals(faktura.KodWaluty, request.KodWaluty, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("Waluta płatności musi być zgodna z walutą faktury.");

                var kwotaBrutto = faktura.KwotaBrutto ?? 0m;
                var sumaRozliczenFaktury = rozliczeniaLookup.TryGetValue(idFaktury, out var suma)
                    ? suma
                    : 0m;

                var kwotaPozostala = kwotaBrutto - sumaRozliczenFaktury;
                if (kwotaPozostala <= 0)
                    throw new InvalidOperationException($"Faktura {faktura.NumerFaktury} nie ma kwoty do rozliczenia.");

                faktury[idFaktury] = faktura;
            }

            foreach (var grupa in request.Rozliczenia.GroupBy(x => x.IdFaktury))
            {
                var faktura = faktury[grupa.Key];
                var sumaRozliczenFaktury = rozliczeniaLookup.TryGetValue(grupa.Key, out var suma)
                    ? suma
                    : 0m;
                var kwotaBrutto = faktura.KwotaBrutto ?? 0m;
                var kwotaPozostala = kwotaBrutto - sumaRozliczenFaktury;
                var sumaNowa = grupa.Sum(x => x.Kwota);

                if (sumaNowa > kwotaPozostala)
                {
                    throw new InvalidOperationException(
                        $"Kwota rozliczenia dla faktury {faktura.NumerFaktury} przekracza pozostałą kwotę.");
                }
            }

            var platnosc = new Platnosc
            {
                IdPodmiotu = request.IdPodmiotu,
                DataPlatnosci = request.DataPlatnosci.Date,
                KodWaluty = request.KodWaluty,
                Kwota = request.Kwota
            };

            await _platnoscRepository.AddAsync(platnosc, ct);

            foreach (var rozliczenie in request.Rozliczenia)
            {
                var faktura = faktury[rozliczenie.IdFaktury];
                var rozliczenieEntity = new RozliczeniePlatnosci
                {
                    Kwota = rozliczenie.Kwota,
                    Platnosc = platnosc,
                    Faktura = faktura,
                    IdFaktury = faktura.IdFaktury
                };

                await _rozliczenieRepository.AddAsync(rozliczenieEntity, ct);
            }

            await _platnoscRepository.SaveChangesAsync(ct);

            return new ZarejestrujPlatnoscResultDto
            {
                IdPlatnosci = platnosc.IdPlatnosci,
                SumaRozliczen = sumaRozliczen
            };
        }
    }
}