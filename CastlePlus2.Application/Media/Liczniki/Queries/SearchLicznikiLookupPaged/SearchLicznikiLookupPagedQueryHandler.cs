using System;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Queries.SearchLicznikiLookupPaged
{
    public sealed class SearchLicznikiLookupPagedQueryHandler
        : IRequestHandler<SearchLicznikiLookupPagedQuery, PagedResultDto<LicznikLookupDto>>
    {
        private readonly ILicznikRepository _licznikRepository;

        public SearchLicznikiLookupPagedQueryHandler(ILicznikRepository licznikRepository)
        {
            _licznikRepository = licznikRepository;
        }

        public async Task<PagedResultDto<LicznikLookupDto>> Handle(SearchLicznikiLookupPagedQuery request, CancellationToken ct)
        {
            var page = request.Page <= 0 ? 1 : request.Page;
            var pageSize = request.PageSize <= 0 ? 20 : Math.Min(request.PageSize, 200);

            var (items, total) = await _licznikRepository.SearchPagedAsync(request.Q, page, pageSize, ct);

            var mapped = items.Select(l => new LicznikLookupDto
            {
                IdLicznika = l.IdLicznika,
                NumerNV = l.NumerNV,
                KodJednostki = l.KodJednostki,
                Aktywny = l.Aktywny,
                Label = $"{l.NumerNV} ({l.KodJednostki}) [Id={l.IdLicznika}]"
            }).ToList();

            return new PagedResultDto<LicznikLookupDto>
            {
                Items = mapped,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}