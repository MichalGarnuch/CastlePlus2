using System.Linq;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.ProcesyRdzen.Queries.GetPrzypisanieAdresuContext
{
    public sealed class GetPrzypisanieAdresuContextQueryHandler
        : IRequestHandler<GetPrzypisanieAdresuContextQuery, PrzypisanieAdresuContextDto>
    {
        private readonly IAdresRepository _adresRepository;

        public GetPrzypisanieAdresuContextQueryHandler(IAdresRepository adresRepository)
        {
            _adresRepository = adresRepository;
        }

        public async Task<PrzypisanieAdresuContextDto> Handle(GetPrzypisanieAdresuContextQuery request, CancellationToken ct)
        {
            var adresy = await _adresRepository.GetAllAsync(ct);

            var adresyLookup = adresy
                .Select(a => new AdresLookupDto
                {
                    IdAdresu = a.IdAdresu,
                    Label = BuildAdresLabel(a)
                })
                .OrderBy(a => a.Label)
                .ToList();

            // Encje celowo NIE są ładowane - wybór encji idzie przez server-side lookup (autocomplete).
            return new PrzypisanieAdresuContextDto
            {
                Encje = new(),
                Adresy = adresyLookup
            };
        }

        private static string BuildAdresLabel(Adres adres)
        {
            if (!string.IsNullOrWhiteSpace(adres.AdresPelny))
                return adres.AdresPelny;

            var ulica = string.IsNullOrWhiteSpace(adres.Ulica) ? string.Empty : adres.Ulica.Trim();
            var numer = string.IsNullOrWhiteSpace(adres.Numer) ? string.Empty : adres.Numer.Trim();
            var kod = string.IsNullOrWhiteSpace(adres.KodPocztowy) ? string.Empty : adres.KodPocztowy.Trim();
            var miejscowosc = string.IsNullOrWhiteSpace(adres.Miejscowosc) ? string.Empty : adres.Miejscowosc.Trim();

            var ulicaNumer = string.Join(" ", new[] { ulica, numer }.Where(s => !string.IsNullOrWhiteSpace(s)));
            var kodMiejscowosc = string.Join(" ", new[] { kod, miejscowosc }.Where(s => !string.IsNullOrWhiteSpace(s)));

            return string.Join(", ", new[] { ulicaNumer, kodMiejscowosc }.Where(s => !string.IsNullOrWhiteSpace(s)));
        }
    }
}
