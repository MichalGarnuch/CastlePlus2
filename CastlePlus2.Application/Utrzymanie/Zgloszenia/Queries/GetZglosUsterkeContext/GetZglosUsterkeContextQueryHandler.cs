using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.Zgloszenia.Queries.GetZglosUsterkeContext
{
    public class GetZglosUsterkeContextQueryHandler
        : IRequestHandler<GetZglosUsterkeContextQuery, ZglosUsterkeContextDto>
    {
        private readonly ILokalRepository _lokalRepository;
        private readonly IBudynekRepository _budynekRepository;

        public GetZglosUsterkeContextQueryHandler(
            ILokalRepository lokalRepository,
            IBudynekRepository budynekRepository)
        {
            _lokalRepository = lokalRepository;
            _budynekRepository = budynekRepository;
        }

        public async Task<ZglosUsterkeContextDto> Handle(GetZglosUsterkeContextQuery request, CancellationToken ct)
        {
            var budynki = await _budynekRepository.GetAllAsync(ct);
            var lokale = await _lokalRepository.GetAllAsync(ct);

            var budynekLookup = budynki
                .Select(b => new BudynekLookupDto
                {
                    IdEncji = b.Id,
                    KodBudynku = b.KodBudynku,
                    Label = b.KodBudynku
                })
                .OrderBy(b => b.KodBudynku)
                .ToList();

            var budynkiById = budynki
                .GroupBy(b => b.Id)
                .ToDictionary(g => g.Key, g => g.First().KodBudynku);

            var lokalLookup = lokale
                .Select(l =>
                {
                    budynkiById.TryGetValue(l.IdBudynku, out var kodBudynku);
                    var label = string.IsNullOrWhiteSpace(kodBudynku)
                        ? l.KodLokalu
                        : $"{kodBudynku} / {l.KodLokalu}";

                    return new LokalLookupDto
                    {
                        IdEncji = l.Id,
                        KodBudynku = kodBudynku ?? string.Empty,
                        KodLokalu = l.KodLokalu,
                        Label = label
                    };
                })
                .OrderBy(l => l.KodBudynku)
                .ThenBy(l => l.KodLokalu)
                .ToList();

            return new ZglosUsterkeContextDto
            {
                BudynkiLookup = budynekLookup,
                LokaleLookup = lokalLookup
            };
        }
    }
}