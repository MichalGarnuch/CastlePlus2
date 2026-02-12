using System;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Adresy.Queries.SearchAdresyLookupPaged
{
    public sealed class SearchAdresyLookupPagedQueryHandler
        : IRequestHandler<SearchAdresyLookupPagedQuery, PagedResultDto<AdresLookupDto>>
    {
        private readonly IAdresRepository _adresRepository;

        public SearchAdresyLookupPagedQueryHandler(IAdresRepository adresRepository)
        {
            _adresRepository = adresRepository;
        }

        public async Task<PagedResultDto<AdresLookupDto>> Handle(SearchAdresyLookupPagedQuery request, CancellationToken ct)
        {
            var page = request.Page <= 0 ? 1 : request.Page;
            var pageSize = request.PageSize <= 0 ? 20 : Math.Min(request.PageSize, 200);

            var (items, total) = await _adresRepository.SearchPagedAsync(request.Q, page, pageSize, ct);

            var mapped = items.Select(a => new AdresLookupDto
            {
                IdAdresu = a.IdAdresu,
                Label = BuildLabel(a)
            }).ToList();

            return new PagedResultDto<AdresLookupDto>
            {
                Items = mapped,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }

        private static string BuildLabel(Domain.Entities.Rdzen.Adres adres)
        {
            var ulica = string.IsNullOrWhiteSpace(adres.Ulica) ? "-" : adres.Ulica.Trim();
            var kod = string.IsNullOrWhiteSpace(adres.KodPocztowy) ? "-" : adres.KodPocztowy.Trim();

            // wersja kompilowalna bez nieistniejących pól (Miasto/NumerBudynku/NumerLokalu)
            return $"{ulica}, {kod} (Id={adres.IdAdresu})";
        }

    }
}