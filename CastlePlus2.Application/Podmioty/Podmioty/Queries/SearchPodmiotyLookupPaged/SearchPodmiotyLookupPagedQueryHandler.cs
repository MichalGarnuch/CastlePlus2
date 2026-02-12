using System;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Podmioty.Queries.SearchPodmiotyLookupPaged
{
    public sealed class SearchPodmiotyLookupPagedQueryHandler
        : IRequestHandler<SearchPodmiotyLookupPagedQuery, PagedResultDto<PodmiotLookupDto>>
    {
        private readonly IPodmiotRepository _podmiotRepository;

        public SearchPodmiotyLookupPagedQueryHandler(IPodmiotRepository podmiotRepository)
        {
            _podmiotRepository = podmiotRepository;
        }

        public async Task<PagedResultDto<PodmiotLookupDto>> Handle(SearchPodmiotyLookupPagedQuery request, CancellationToken ct)
        {
            var page = request.Page <= 0 ? 1 : request.Page;
            var pageSize = request.PageSize <= 0 ? 20 : Math.Min(request.PageSize, 200);

            var (items, total) = await _podmiotRepository.GetPagedAsync(
                page,
                pageSize,
                request.Q,
                "Nazwa",
                false,
                ct);

            var mapped = items.Select(p => new PodmiotLookupDto
            {
                IdPodmiotu = p.IdPodmiotu,
                Nazwa = p.Nazwa,
                NIP = p.NIP,
                REGON = p.REGON,
                PESEL = p.PESEL,
                TypPodmiotu = p.TypPodmiotu,
                Label = BuildLabel(p)
            }).ToList();

            return new PagedResultDto<PodmiotLookupDto>
            {
                Items = mapped,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }

        private static string BuildLabel(Domain.Entities.Podmioty.Podmiot podmiot)
        {
            var nip = string.IsNullOrWhiteSpace(podmiot.NIP) ? "-" : podmiot.NIP;
            return $"{podmiot.Nazwa} (NIP: {nip})";
        }
    }
}