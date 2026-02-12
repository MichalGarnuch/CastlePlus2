using System;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.ProcesyRdzen.Queries.SearchEncjeLookupPaged
{
    public sealed class SearchEncjeLookupPagedQueryHandler
        : IRequestHandler<SearchEncjeLookupPagedQuery, PagedResultDto<EncjaLookupDto>>
    {
        private readonly IEncjaRepository _encjaRepository;

        public SearchEncjeLookupPagedQueryHandler(IEncjaRepository encjaRepository)
        {
            _encjaRepository = encjaRepository;
        }

        public async Task<PagedResultDto<EncjaLookupDto>> Handle(SearchEncjeLookupPagedQuery request, CancellationToken ct)
        {
            var (items, total) = await _encjaRepository.SearchPagedAsync(
                request.TypEncji,
                request.Q,
                request.Page,
                request.PageSize,
                ct);

            var mapped = items.Select(e => new EncjaLookupDto
            {
                IdEncji = e.Id,
                TypEncji = e.TypEncji,
                KodEncji = e.KodEncji,
                Label = string.IsNullOrWhiteSpace(e.KodEncji)
                    ? e.TypEncji
                    : $"{e.TypEncji} / {e.KodEncji}"
            }).ToList();

            return new PagedResultDto<EncjaLookupDto>
            {
                Items = mapped,
                TotalCount = total,
                Page = request.Page <= 0 ? 1 : request.Page,
                PageSize = request.PageSize <= 0 ? 20 : Math.Min(request.PageSize, 200)
            };
        }
    }
}