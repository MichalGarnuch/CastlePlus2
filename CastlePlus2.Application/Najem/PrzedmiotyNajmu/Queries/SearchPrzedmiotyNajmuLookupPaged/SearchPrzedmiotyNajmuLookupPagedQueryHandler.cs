using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;
using System;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Queries.SearchPrzedmiotyNajmuLookupPaged
{
    public sealed class SearchPrzedmiotyNajmuLookupPagedQueryHandler
        : IRequestHandler<SearchPrzedmiotyNajmuLookupPagedQuery, PagedResultDto<PrzedmiotNajmuLookupDto>>
    {
        private readonly IPrzedmiotNajmuRepository _przedmiotRepository;
        private readonly IEncjaRepository _encjaRepository;

        public SearchPrzedmiotyNajmuLookupPagedQueryHandler(
            IPrzedmiotNajmuRepository przedmiotRepository,
            IEncjaRepository encjaRepository)
        {
            _przedmiotRepository = przedmiotRepository;
            _encjaRepository = encjaRepository;
        }

        public async Task<PagedResultDto<PrzedmiotNajmuLookupDto>> Handle(SearchPrzedmiotyNajmuLookupPagedQuery request, CancellationToken ct)
        {
            var page = request.Page <= 0 ? 1 : request.Page;
            var pageSize = request.PageSize <= 0 ? 20 : Math.Min(request.PageSize, 200);

            var (items, total) = await _przedmiotRepository.SearchPagedAsync(
                request.Q,
                request.IdUmowyNajmu,
                page,
                pageSize,
                ct);

            var mapped = new List<PrzedmiotNajmuLookupDto>();
            foreach (var item in items)
            {
                var encja = await _encjaRepository.GetByIdAsync(item.IdEncji, ct);
                var encjaLabel = encja is null
                    ? item.IdEncji.ToString()
                    : string.IsNullOrWhiteSpace(encja.KodEncji)
                        ? encja.TypEncji
                        : $"{encja.TypEncji} / {encja.KodEncji}";

                mapped.Add(new PrzedmiotNajmuLookupDto
                {
                    IdPrzedmiotuNajmu = item.IdPrzedmiotuNajmu,
                    IdUmowyNajmu = item.IdUmowyNajmu,
                    IdEncji = item.IdEncji,
                    EncjaLabel = encjaLabel,
                    Label = $"Id={item.IdPrzedmiotuNajmu} / {encjaLabel}"
                });
            }

            return new PagedResultDto<PrzedmiotNajmuLookupDto>
            {
                Items = mapped,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}