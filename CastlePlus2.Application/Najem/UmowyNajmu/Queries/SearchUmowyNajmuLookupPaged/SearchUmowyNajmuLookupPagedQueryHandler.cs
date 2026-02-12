using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;
using System;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Queries.SearchUmowyNajmuLookupPaged
{
    public sealed class SearchUmowyNajmuLookupPagedQueryHandler
        : IRequestHandler<SearchUmowyNajmuLookupPagedQuery, PagedResultDto<UmowaNajmuLookupDto>>
    {
        private readonly IUmowaNajmuRepository _umowaRepository;

        public SearchUmowyNajmuLookupPagedQueryHandler(IUmowaNajmuRepository umowaRepository)
        {
            _umowaRepository = umowaRepository;
        }

        public async Task<PagedResultDto<UmowaNajmuLookupDto>> Handle(SearchUmowyNajmuLookupPagedQuery request, CancellationToken ct)
        {
            var page = request.Page <= 0 ? 1 : request.Page;
            var pageSize = request.PageSize <= 0 ? 20 : Math.Min(request.PageSize, 200);

            var (items, total) = await _umowaRepository.SearchPagedAsync(request.Q, page, pageSize, ct);

            var mapped = items.Select(u => new UmowaNajmuLookupDto
            {
                IdUmowy = u.Id,
                NumerUmowy = u.KodEncji ?? string.Empty,
                DataPoczatku = u.DataPoczatku,
                DataZakonczenia = u.DataZakonczenia,
                Label = BuildLabel(u)
            }).ToList();

            return new PagedResultDto<UmowaNajmuLookupDto>
            {
                Items = mapped,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }

        private static string BuildLabel(Domain.Entities.Najem.UmowaNajmu umowa)
        {
            var numer = string.IsNullOrWhiteSpace(umowa.KodEncji) ? umowa.Id.ToString() : umowa.KodEncji;
            var koniec = umowa.DataZakonczenia is null ? "bezterminowo" : umowa.DataZakonczenia.Value.ToString("yyyy-MM-dd");
            return $"{numer} ({umowa.DataPoczatku:yyyy-MM-dd} → {koniec})";
        }
    }
}