using System;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Queries.SearchFakturyLookupPaged
{
    public sealed class SearchFakturyLookupPagedQueryHandler
        : IRequestHandler<SearchFakturyLookupPagedQuery, PagedResultDto<FakturaLookupDto>>
    {
        private readonly IFakturaRepository _fakturaRepository;

        public SearchFakturyLookupPagedQueryHandler(IFakturaRepository fakturaRepository)
        {
            _fakturaRepository = fakturaRepository;
        }

        public async Task<PagedResultDto<FakturaLookupDto>> Handle(SearchFakturyLookupPagedQuery request, CancellationToken ct)
        {
            var page = request.Page <= 0 ? 1 : request.Page;
            var pageSize = request.PageSize <= 0 ? 20 : Math.Min(request.PageSize, 200);

            var (items, total) = await _fakturaRepository.SearchPagedAsync(
                request.Q,
                request.IdPodmiotu,
                page,
                pageSize,
                ct);

            var mapped = items.Select(f => new FakturaLookupDto
            {
                IdFaktury = f.IdFaktury,
                NumerFaktury = f.NumerFaktury,
                IdPodmiotu = f.IdPodmiotu,
                DataWystawienia = f.DataWystawienia,
                KwotaBrutto = f.KwotaBrutto,
                Label = $"{f.NumerFaktury} ({f.DataWystawienia:yyyy-MM-dd}) [Id={f.IdFaktury}]"
            }).ToList();

            return new PagedResultDto<FakturaLookupDto>
            {
                Items = mapped,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}