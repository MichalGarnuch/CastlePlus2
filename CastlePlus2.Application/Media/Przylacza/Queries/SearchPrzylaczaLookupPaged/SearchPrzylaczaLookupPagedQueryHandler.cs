using System;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Queries.SearchPrzylaczaLookupPaged
{
    public sealed class SearchPrzylaczaLookupPagedQueryHandler
        : IRequestHandler<SearchPrzylaczaLookupPagedQuery, PagedResultDto<PrzylaczeLookupDto>>
    {
        private readonly IPrzylaczeRepository _przylaczeRepository;

        public SearchPrzylaczaLookupPagedQueryHandler(IPrzylaczeRepository przylaczeRepository)
        {
            _przylaczeRepository = przylaczeRepository;
        }

        public async Task<PagedResultDto<PrzylaczeLookupDto>> Handle(SearchPrzylaczaLookupPagedQuery request, CancellationToken ct)
        {
            var page = request.Page <= 0 ? 1 : request.Page;
            var pageSize = request.PageSize <= 0 ? 20 : Math.Min(request.PageSize, 200);

            var (items, total) = await _przylaczeRepository.SearchPagedAsync(request.Q, page, pageSize, ct);

            var mapped = items.Select(p => new PrzylaczeLookupDto
            {
                IdPrzylacza = p.IdPrzylacza,
                IdEncjiGospodarza = p.IdEncjiGospodarza,
                KodRodzaju = p.KodRodzaju,
                Opis = p.Opis,
                Label = $"Id={p.IdPrzylacza} / Rodzaj={p.KodRodzaju}"
            }).ToList();

            return new PagedResultDto<PrzylaczeLookupDto>
            {
                Items = mapped,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}