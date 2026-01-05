using System.Linq;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.ProcesyRdzen.Queries.SearchEncjeLookup
{
    public sealed class SearchEncjeLookupQueryHandler
        : IRequestHandler<SearchEncjeLookupQuery, List<EncjaLookupDto>>
    {
        private readonly IEncjaRepository _encjaRepository;

        public SearchEncjeLookupQueryHandler(IEncjaRepository encjaRepository)
        {
            _encjaRepository = encjaRepository;
        }

        public async Task<List<EncjaLookupDto>> Handle(SearchEncjeLookupQuery request, CancellationToken ct)
        {
            var take = request.Take <= 0 ? 50 : Math.Min(request.Take, 200);
            var encje = await _encjaRepository.SearchAsync(request.TypEncji, request.Q, take, ct);

            return encje
                .OrderBy(e => e.TypEncji)
                .ThenBy(e => e.KodEncji)
                .Select(e => new EncjaLookupDto
                {
                    IdEncji = e.Id,
                    TypEncji = e.TypEncji,
                    KodEncji = e.KodEncji,
                    Label = string.IsNullOrWhiteSpace(e.KodEncji)
                        ? e.TypEncji
                        : $"{e.TypEncji} / {e.KodEncji}"
                })
                .ToList();
        }
    }
}
