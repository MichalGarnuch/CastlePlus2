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
            return new ZglosUsterkeContextDto
            {
                BudynkiLookup = new List<BudynekLookupDto>(),
                LokaleLookup = new List<LokalLookupDto>()
            };
        }
    }
}