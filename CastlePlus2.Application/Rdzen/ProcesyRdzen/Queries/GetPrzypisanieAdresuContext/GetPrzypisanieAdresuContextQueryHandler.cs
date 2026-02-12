using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.ProcesyRdzen.Queries.GetPrzypisanieAdresuContext
{
    public sealed class GetPrzypisanieAdresuContextQueryHandler
        : IRequestHandler<GetPrzypisanieAdresuContextQuery, PrzypisanieAdresuContextDto>
    {
        private readonly IAdresRepository _adresRepository;

        public GetPrzypisanieAdresuContextQueryHandler(IAdresRepository adresRepository)
        {
            _adresRepository = adresRepository;
        }

        public async Task<PrzypisanieAdresuContextDto> Handle(GetPrzypisanieAdresuContextQuery request, CancellationToken ct)
        {
            // Encje celowo NIE są ładowane - wybór encji idzie przez server-side lookup (autocomplete).
            return new PrzypisanieAdresuContextDto
            {
                Encje = new(),
                Adresy = new List<AdresLookupDto>()
            };
        }
    }
}